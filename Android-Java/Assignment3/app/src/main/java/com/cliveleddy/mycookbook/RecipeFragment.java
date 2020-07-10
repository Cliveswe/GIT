package com.cliveleddy.mycookbook;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.provider.ContactsContract;
import android.provider.MediaStore;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.ShareCompat;
import android.support.v4.content.FileProvider;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Toast;

import java.io.File;
import java.util.Date;
import java.util.List;
import java.util.Objects;
import java.util.UUID;

import model.CookBook;
import model.Recipe;
import model.RecipeCategories;

/**
 * RecipeFragment is a controller the interacts with a model and view objects. Here it presents a
 * specific recipe and updates the details of the recipe via user interaction.
 */
@SuppressWarnings("NullableProblems")
public class RecipeFragment extends Fragment{
    //region Fields
    private static final String ARG_RECIPE_ID = "recipe_id";
    private static final String DIALOG_DATE = "DialogDate";
    private static final int REQUEST_DATE = 0;
    private static final int REQUEST_CONTACT = 1;
    private static final int REQUEST_PHOTO = 2;
    private static final String DIALOG_PHOTO = "DialogPhoto";
    private static final String AUTHORITY = "com.cliveleddy.mycookbook.fileprovider";

    //endregion

    //region layout elements
    private Recipe mRecipe;
    @SuppressWarnings("FieldCanBeLocal")
    private EditText mName;
    @SuppressWarnings("FieldCanBeLocal")
    private EditText mIngredientsField;
    @SuppressWarnings("FieldCanBeLocal")
    private EditText mInstructionsField;
    private Button mDateButton;
    @SuppressWarnings("FieldCanBeLocal")
    private Button mRecipeDetailsButton;
    private Button mContactButton;
    @SuppressWarnings("FieldCanBeLocal")
    private ImageButton mPhotoButton;
    private ImageView mPhotoView;
    private File mPhotoFile;
    @SuppressWarnings("FieldCanBeLocal")
    private Spinner mCategorySpinner;
    //endregion

    /**
     * To attach arguments to a fragment it must be done after the fragment is created but before
     * it is added to and activity. That is where this method comes in. It bundles up and sets its
     * arguments.
     * @param recipeId UUID
     * @return a fragment RecipeFragment
     */
    public static RecipeFragment newInstance(UUID recipeId){
        Bundle args = new Bundle();//attach a bundle object to the fragment
        args.putSerializable(ARG_RECIPE_ID, recipeId);//add a key value pair of arguments

        RecipeFragment fragment = new RecipeFragment();
        fragment.setArguments(args);//attach the arguments to the fragment

        return fragment;
    }

    /**
     * An activity wants to host this fragment. Configure the fragments instance.
     * @param savedInstanceState the fragments saved state.
     */
    @Override
    public void onCreate(Bundle savedInstanceState){
        super.onCreate(savedInstanceState);
        setHasOptionsMenu(true);

        //fragment needs to access its arguments
        assert getArguments() != null;
        UUID recipeId = (UUID) getArguments().getSerializable(ARG_RECIPE_ID);

        //fetch the recipe from the cookbook
        mRecipe = CookBook.get(getActivity()).getRecipe(recipeId);
        //get the location of the photo
        mPhotoFile = CookBook.get(getActivity()).getPhotoFile(mRecipe);
    }

    @Override
    public void onPause(){
        super.onPause();

        CookBook.get(getActivity()).updateRecipe(mRecipe);
    }

    /**
     * Respond to selected item of the menu items.
     * @param item MenuItem
     * @return true if ok else super.onOptionsItemSelected(item)
     */
    @Override
    public boolean onOptionsItemSelected(MenuItem item){
        switch (item.getItemId()){
            case R.id.delete_recipe:
                CookBook cookBook = CookBook.get(getActivity());
                cookBook.deleteRecipe(mRecipe);
                Objects.requireNonNull(getActivity()).finish();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }

    }

    /**
     * Inflate the layout for the fragments view to the hosting activity.
     * @param inflater inflate the fragment
     * @param container view's parent
     * @param savedInstanceState Bundle that contains data that can be used to re-create the view
     *                           from the saved state.
     * @return View the layout view.
     */
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState){

        //inflate the fragment, false because the view will be added in the activity code
        View v = inflater.inflate(R.layout.fragment_recipe, container, false);

        //region Recipe fragment i/o

        //region recipe name
        mName = v.findViewById(R.id.recipe_name);
        mName.setText(mRecipe.getName());
        mName.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
                //This is intentionally left blank
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                mRecipe.setName(s.toString());
            }

            @Override
            public void afterTextChanged(Editable s) {
                //This is intentionally left blank
            }
        });
        //endregion

        //region recipe ingredients
        mIngredientsField = v.findViewById(R.id.recipe_ingredients);
        mIngredientsField.setText(mRecipe.getIngredients());
        mIngredientsField.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
                //This is intentionally left blank
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                mRecipe.setIngredients(s.toString());
            }

            @Override
            public void afterTextChanged(Editable s) {
                //This is intentionally left blank
            }
        });

        //recipe instructions
        mInstructionsField = v.findViewById(R.id.recipe_instructions);
        mInstructionsField.setText(mRecipe.getInstructions());
        mInstructionsField.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {
                //This is intentionally left blank
            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                mRecipe.setInstructions(s.toString());
            }

            @Override
            public void afterTextChanged(Editable s) {
                //This is intentionally left blank
            }
        });
        //endregion

        //region recipe date
        mDateButton = v.findViewById(R.id.recipe_date);
        updateDate();
        mDateButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentManager manager = getFragmentManager();
                DatePickerFragment dialog = DatePickerFragment
                        .newInstance(mRecipe.getDate());
                //returning data to RecipeFragment this is the target
                dialog.setTargetFragment(RecipeFragment.this, REQUEST_DATE);
                assert manager != null;
                dialog.show(manager, DIALOG_DATE);
            }
        });
        //endregion

        //endregion

        //region Implicit intent send a recipe
        mRecipeDetailsButton = v.findViewById(R.id.recipe_text);
        mRecipeDetailsButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //a general action user decides what to do next
                Intent intent = ShareCompat.IntentBuilder.from(
                        Objects.requireNonNull(getActivity()))
                        .setType(getString(R.string.intent_action_send))
                        .setChooserTitle(getString(R.string.send_recipe))
                        .setSubject(getString(R.string.recipe_details_name_text))
                        .setText(getRecipeDetails())
                        .createChooserIntent();
                startActivity(intent);
            }
        });

        //endregion

        //region Implicit intent choose a guest
        //pick an item from in the contacts database
        final Intent pickContact = new Intent(Intent.ACTION_PICK,
                ContactsContract.Contacts.CONTENT_URI);
        mContactButton = v.findViewById(R.id.invite_guest);
        mContactButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivityForResult(pickContact,
                        REQUEST_CONTACT);
            }
        });

        if(mRecipe.getGuest() != null){
            mContactButton.setText(mRecipe.getGuest());
        }
        //endregion

        //region Check for responding activities - PackageManager know all the installed components
        //on a device including all activities
        PackageManager packageManager = Objects.requireNonNull(getActivity()).getPackageManager();
        //ask the packageManager to locate an activity that matches the intent
        if(packageManager.resolveActivity(pickContact,
                PackageManager.MATCH_DEFAULT_ONLY) == null){
            mContactButton.setEnabled(false);
        }
        //endregion

        //region Image control
        //respond to the image controls
        mPhotoButton = v.findViewById(R.id.recipe_camera);
        final Intent captureImage = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);

        //disable the photo button if there is no camera app or storage location for photo
        boolean canTakePhoto = (mPhotoFile != null) &&
                (captureImage.resolveActivity(packageManager) != null);
        mPhotoButton.setEnabled(canTakePhoto);

        //take a photo
        mPhotoButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //translate local file path to Uri for the camera operation
                Uri uri = FileProvider.getUriForFile(getActivity(), AUTHORITY, mPhotoFile);
                captureImage.putExtra(MediaStore.EXTRA_OUTPUT, uri);
                List<ResolveInfo> cameraActivities = getActivity()
                        .getPackageManager().queryIntentActivities(captureImage,
                                PackageManager.MATCH_DEFAULT_ONLY);
                //grant camera permission for every activity the cameraImage intent can resolve
                for (ResolveInfo activity: cameraActivities){
                    getActivity().grantUriPermission(activity.activityInfo.packageName,
                            uri, Intent.FLAG_GRANT_WRITE_URI_PERMISSION);
                }
                startActivityForResult(captureImage, REQUEST_PHOTO);
            }
        });
        mPhotoView = v.findViewById(R.id.recipe_photo);
        updatePhotoView();
        //endregion

        //region spinner control
        mCategorySpinner = v.findViewById(R.id.spinner_category);

        //create spinner adapter
        ArrayAdapter<RecipeCategories> adapter = new ArrayAdapter<> (
                Objects.requireNonNull(getContext()),
                R.layout.custom_spinner, RecipeCategories.values());

        adapter.setDropDownViewResource(R.layout.custom_spinner);
        mCategorySpinner.setAdapter(adapter);

        if(RecipeCategories.elementOf(mRecipe.getCategory()) != null) {
            //select the spinner category from recipe
            mCategorySpinner.setSelection(RecipeCategories.elementOf(mRecipe.getCategory()).ordinal());
        }

        //add a listener to determine recipe category
        mCategorySpinner.setOnItemSelectedListener(new OnItemSelectedListener() {
            /**
             * A category has been selected from the spinner.
             *
             * @param parent   Spinner adapter.
             * @param view
             * @param position Spinner item selected at position as int.
             * @param id
             */
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                if (view != null) {
                    mRecipe.setCategory(parent.getItemAtPosition(position).toString());
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                //Do nothing
            }
        });

        //endregion
        return v;
    }


    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if(resultCode != Activity.RESULT_OK){
            return;
        }
        if (requestCode == REQUEST_DATE){
            Date date = (Date) data.getSerializableExtra(DatePickerFragment.EXTRA_DATE);
            mRecipe.setDate(date);
            updateDate();
        }else if ((requestCode == REQUEST_CONTACT) && (data != null)){
            Uri contactUri = data.getData();
            //specify which fields you want your query to return values for
            String[] queryFields = new String[]{
                    ContactsContract.Contacts.DISPLAY_NAME};

            //perform a query - the contactUri is similar to "where" clause
            Cursor c = Objects.requireNonNull(getActivity()).getContentResolver()
                    .query(Objects.requireNonNull(contactUri), queryFields,null, null, null);

            try {
                //check the results
                assert c != null;
                if(c.getCount() == 0){
                    return;
                }
                //retrieve the first column from the first row - this is the guests name
                c.moveToFirst();
                String guest = c.getString(0);
                mRecipe.setGuest(guest);
                mContactButton.setText(guest);
            }finally {
                assert c != null;
                c.close();
            }
        }else if(requestCode == REQUEST_PHOTO){
            Uri uri = FileProvider.getUriForFile(Objects.requireNonNull(getActivity()), AUTHORITY, mPhotoFile);
            getActivity().revokeUriPermission(uri, Intent.FLAG_GRANT_WRITE_URI_PERMISSION);
            updatePhotoView();
        }
    }

    /**
     * Add a menu.
     * @param menu Menu
     * @param inflater MenuInflater
     */
    @Override
    public void onCreateOptionsMenu(Menu menu, MenuInflater inflater){
        super.onCreateOptionsMenu(menu, inflater);
        inflater.inflate(R.menu.fragment_recipe, menu);
    }

    private void updateDate() {
        mDateButton.setText(mRecipe.getDateAsFormattedString());
    }

    /**
     * Generate a dinner invitations with suggested recipe.
     * @return invitation as a String
     */
    private String getRecipeDetails(){
        String guest = mRecipe.getGuest();
        if(guest == null){
            guest = getString(R.string.recipe_details_no_guest);
        }else {
            guest = getString(R.string.recipe_details_guest, guest);
        }
        return guest +
                getString(R.string.recipe_details_text,
                        mRecipe.getName(),
                        mRecipe.getCategory(),
                        mRecipe.getDateAsFormattedString(),
                        mRecipe.getIngredients(),
                        mRecipe.getInstructions());
    }

    /**
     * Load a Bitmap into the ImageView.
     */
    private void updatePhotoView(){
        if((mPhotoFile == null) || !mPhotoFile.exists()){

            mPhotoView.setImageDrawable(getResources()
                    .getDrawable(R.drawable.ic_menu_insert_photo, null));
            //region Missing Photo listener
            mPhotoView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Context context = getActivity();
                    int duration = Toast.LENGTH_LONG;

                    Toast toast = Toast.makeText(context,
                            getString(R.string.take_photo), duration);
                    toast.setGravity(Gravity.CENTER_VERTICAL|Gravity.BOTTOM, 0, 0);
                    toast.show();
                }

            });
            //endregion

        }else{
            Bitmap bitmap = PictureUtils.getScaledBitmap(mPhotoFile.getPath(),
                    Objects.requireNonNull(getActivity()));

            mPhotoView.setImageBitmap(bitmap);
            //mPhotoView.setBackground(null);

            //region Photo AlertDialog on ImageView listener
            mPhotoView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    FragmentManager manager = getFragmentManager();
                    PhotoDialogFragment dialog = PhotoDialogFragment.newInstance(mPhotoFile);

                    assert manager != null;
                    dialog.show(manager, DIALOG_PHOTO);
                }
            });
            //endregion

        }

    }
}
