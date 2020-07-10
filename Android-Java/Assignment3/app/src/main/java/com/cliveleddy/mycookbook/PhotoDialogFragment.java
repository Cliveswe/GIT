package com.cliveleddy.mycookbook;

import android.annotation.SuppressLint;
import android.app.Dialog;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.support.annotation.NonNull;

import android.support.v4.app.DialogFragment;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.widget.ImageView;

import java.io.File;
import java.util.Objects;

/**
 * Wrap the AlertDialog in a DialogFragment to allow more options for presenting the Dialog.
 * One advantage of wrapping is the the AlertDialog is re-created when the device is rotated.
 */
public class PhotoDialogFragment extends DialogFragment {
    private static final String ARG_File = "photo";

    /**
     * Create and set the fragment.
     * @param file A File
     * @return
     */
    public static PhotoDialogFragment newInstance(File  file){
        Bundle args = new Bundle();
        args.putSerializable(ARG_File, file);

        PhotoDialogFragment fragment = new PhotoDialogFragment();
        fragment.setArguments(args);

        return fragment;
    }

    /**
     * Create an instance of the AlertDialog by passing a context into the AlertDialog.Builder.
     * Where after configure the dialog with a title and a positive "Ok" button. The button accepts
     * a string and implements DialogInterface.OnClickListener.
     * @param savedInstanceState
     * @return
     */
    @NonNull
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {

        @SuppressLint("InflateParams") View v = LayoutInflater.from(getActivity())
                .inflate(R.layout.dialog_detail_photo, null);
        //retrieve the File
        assert getArguments() != null;
        File photoFile = (File)getArguments().getSerializable(ARG_File);
        //add the photo to the ImageView
        ImageView mPhoto = v.findViewById(R.id.detail_photo);
        Bitmap bitmap = PictureUtils.getScaledBitmap(photoFile != null ?
                photoFile.getPath() : null, Objects.requireNonNull(getActivity()));
        mPhoto.setImageBitmap(bitmap);

        //create an AlertDialog
        final AlertDialog dialog =  new AlertDialog.Builder(getActivity(),
                android.R.style.Theme_Light_NoTitleBar_Fullscreen)
                .setView(v)
                .create();

        //add a view to OnTouchListener
        v.setOnTouchListener(new View.OnTouchListener() {
            @SuppressLint("ClickableViewAccessibility")
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                //motion up or down
                if(event.getAction() == MotionEvent.ACTION_MOVE) {
                    dialog.dismiss();//close the AlertDialog
                }
                return true;
            }
        });
        return dialog;
    }



}
