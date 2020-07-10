package com.cliveleddy.mycookbook;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v4.app.DialogFragment;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.DatePicker;

import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.Objects;

public class DatePickerFragment extends DialogFragment {
    private static final String ARG_DATE = "date";
    private DatePicker mDatePicker;
    public static final String EXTRA_DATE = "com.cliveleddy.mycookbook.date";

    /**
     * Have the DatePickerFragment pass the date to RecipeFragment via intent.
     * @param resultCode
     * @param date
     */
    private void sendResult(@SuppressWarnings("SameParameterValue") int resultCode, Date date){
        if (getTargetFragment() == null){
            return;
        }

        Intent intent = new Intent();
        intent.putExtra(EXTRA_DATE, date);
        getTargetFragment().onActivityResult(getTargetRequestCode(), resultCode, intent);
    }

    /**
     * Stash the date in DatePickerFragments arguments bundle.
     * @param date Date of recipe.
     * @return
     */
    public static DatePickerFragment newInstance(Date date){
        //stash the date in the arguments bundle
        Bundle args = new Bundle();
        args.putSerializable(ARG_DATE, date);

        DatePickerFragment fragment = new DatePickerFragment();
        fragment.setArguments(args);
        return fragment;
    }

    /**
     * Build an AlertDialog with a title and ok button.
     * @param savedInstanceState
     * @return
     */
    @NonNull
    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        assert getArguments() != null;
        Date date = (Date) getArguments().getSerializable(ARG_DATE);

        //initialize the date
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(date);
        int year = calendar.get(Calendar.YEAR);
        int month = calendar.get(Calendar.MONTH);
        int day = calendar.get(Calendar.DAY_OF_MONTH);

        //inflate the view
        @SuppressLint("InflateParams") View v = LayoutInflater.from(getActivity())
                .inflate(R.layout.dialog_date, null);

        //show the date
        mDatePicker = v.findViewById(R.id.dialog_date_picker);
        mDatePicker.init(year, month,day, null);

        //first get an instance of the AlertDialog.Builder then set the view
        //give it a title and give the button a string resource and object that implements the
        //onClickListener
        return new AlertDialog.Builder(Objects.requireNonNull(getActivity()))
                .setView(v)
                .setTitle(R.string.date_picker_title)
                .setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        int year = mDatePicker.getYear();
                        int month = mDatePicker.getMonth();
                        int day = mDatePicker.getDayOfMonth();
                        Date date = new GregorianCalendar(year, month, day).getTime();
                        sendResult(Activity.RESULT_OK, date);
                    }
                })
                .create();
    }
}
