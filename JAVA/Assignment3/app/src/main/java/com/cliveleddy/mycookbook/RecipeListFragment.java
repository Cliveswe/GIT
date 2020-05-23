package com.cliveleddy.mycookbook;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.constraint.ConstraintLayout;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.List;
import java.util.Objects;

import model.CookBook;
import model.Recipe;

public class RecipeListFragment extends Fragment {
    private static final String SAVED_SUBTITLE_VISIBLE = "subtitle";
    private RecyclerView mRecipeRecyclerView;
    private RecipeAdapter mAdapter;
    private ConstraintLayout mEmptyCookBookView;
    private boolean mSubtitleVisible;

    /**
     * Explicitly tell the FragmentManager that it should receive a call to onCreateOptionsMenu...
     * @param savedInstanceState Bundle
     */
    @Override
    public void onCreate(Bundle savedInstanceState){
        super.onCreate(savedInstanceState);
        setHasOptionsMenu(true);
    }

    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState){
        View view = inflater.inflate(R.layout.fragment_recipe_list, container, false);
        mRecipeRecyclerView = view.findViewById(R.id.recipe_recycler_view);
        mRecipeRecyclerView.setLayoutManager(new LinearLayoutManager(getActivity()));

        mEmptyCookBookView = view.findViewById(R.id.empty_layout);

        if(savedInstanceState != null){
            mSubtitleVisible = savedInstanceState.getBoolean(SAVED_SUBTITLE_VISIBLE);
        }

        updateUI();

        return view;
    }

    /**
     * When RecipeListFragment is resumed, it calls onResume() from the OS.
     */
    @Override
    public void onResume(){
        super.onResume();
        updateUI();
    }

    @Override
    public void onSaveInstanceState(@NonNull Bundle outState){
        super.onSaveInstanceState(outState);
        outState.putBoolean(SAVED_SUBTITLE_VISIBLE, mSubtitleVisible);
    }

    /**
     * Create a menu in the fragment.
     * @param menu Menu instance
     * @param inflater
     */
    @Override
    public void onCreateOptionsMenu(Menu menu, MenuInflater inflater){
        super.onCreateOptionsMenu(menu, inflater);
        inflater.inflate(R.menu.fragment_recipe_list, menu);

        //Toggling the action item title to show or hide the subtitle
        MenuItem subtitleItem = menu.findItem(R.id.show_subtitle);
        if(mSubtitleVisible){
            subtitleItem.setTitle(R.string.hide_subtitle);
        }else {
            subtitleItem.setTitle(R.string.show_subtitle);
        }
    }

    /**
     * Respond to selected item of the menu items.
     * @param item MenuItem
     * @return
     */
    @Override
    public boolean onOptionsItemSelected(MenuItem item){
        switch (item.getItemId()){
            case R.id.new_recipe:
                Recipe recipe = new Recipe();
                CookBook.get(getActivity()).addRecipe(recipe);
                Intent intent = RecipePagerActivity.newIntent(getActivity(), recipe.getId());
                startActivity(intent);
                return true;
            case R.id.show_subtitle:
                mSubtitleVisible = !mSubtitleVisible;
                Objects.requireNonNull(getActivity()).invalidateOptionsMenu();
                updateSubtitle();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }

    /**
     * Show the number of recipes in the CookBook.
     */
    private void updateSubtitle(){
        CookBook cookBook = CookBook.get(getActivity());
        int recipeCount = cookBook.getRecipes().size();
        //String subtitle = getString(R.string.subtitle_format, recipeCount);
        String subtitle = getResources()
                .getQuantityString(R.plurals.subtitle_plural, recipeCount, recipeCount);

        if(!mSubtitleVisible){
            subtitle = null;
        }

        AppCompatActivity activity = (AppCompatActivity) getActivity();
        assert activity != null;
        Objects.requireNonNull(activity.getSupportActionBar()).setSubtitle(subtitle);
    }

    /**
     * Update the UI of the fragments view.
     */
    private void updateUI(){
        CookBook cookBook = CookBook.get(getActivity());
        List<Recipe> recipes = cookBook.getRecipes();

        //set an instruction on start up or remove it
        mEmptyCookBookView.setVisibility(recipes.size()> 0 ? View.GONE : View.VISIBLE);

        if(mAdapter == null){
            //send a list of recipes to the adapter
            mAdapter = new RecipeAdapter(recipes);
            //inform the RecyclerView what adapter to connect to
            mRecipeRecyclerView.setAdapter(mAdapter);
        }else {
            mAdapter.setRecipes(recipes);
            mAdapter.notifyDataSetChanged();
        }

        updateSubtitle();
    }

    //region Class Holder
    /**
     * A view holder that will inflate the items layout. It also implements a on click listener
     * for interaction.
     */
    private class RecipeHolder extends RecyclerView.ViewHolder implements View.OnClickListener{
        //binding to a view
        private final TextView mTitleTextView;
        private final TextView mDateTextView;
        private final TextView mCategoryTextView;
        private Recipe mRecipe;


        RecipeHolder(LayoutInflater inflater, ViewGroup parent){
            super(inflater.inflate(R.layout.list_item_recipe, parent, false));

            itemView.setOnClickListener(this);//add a click listener to the holder

            mTitleTextView = itemView.findViewById(R.id.recipe_title);
            mDateTextView = itemView.findViewById(R.id.recipe_date);
            mCategoryTextView = itemView.findViewById(R.id.recipe_category);

        }

        /**
         * Bind method that is called every time a new recipe should be displayed in the RecipeHolder.
         * @param recipe Recipe object.
         */
        void bind(Recipe recipe){
            mRecipe = recipe;

            mTitleTextView.setText(mRecipe.getName());
            mDateTextView.setText(mRecipe.getDateAsFormattedString());
            mCategoryTextView.setText(mRecipe.getCategory());
        }

        /**
         * The holder can now be interactive.
         * @param v
         */
        @Override
        public void onClick(View v) {
            Intent intent = RecipePagerActivity.newIntent(getActivity(), mRecipe.getId());
            startActivity(intent);
        }
    }
    //endregion

    //region Class Adapter
    /**
     * The adapter provides a service to the RecyclerView. The adapter has knowledge about both the
     * model and the ViewHolder.
     */
    private class RecipeAdapter extends RecyclerView.Adapter<RecipeHolder>{
        private List<Recipe> mRecipes;

        RecipeAdapter(List<Recipe> recipes){
            mRecipes = recipes;
        }


        /**
         * Called by the RecyclerView when it needs a new ViewHolder to display an item.
         * @param parent
         * @param viewType
         * @return
         */
        @NonNull
        @Override
        public RecipeHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
            //construct a new RecipeHolder
            LayoutInflater layoutInflater = LayoutInflater.from(getActivity());
            return new RecipeHolder(layoutInflater, parent);
        }

        /**
         * Bind a recipe every time the RecyclerView requests that a given RecipeHolder be bound
         * to a particular recipe.
         * @param holder
         * @param position a position in the list of RecyclerView
         */
        @Override
        public void onBindViewHolder(@NonNull RecipeHolder holder, int position) {
            Recipe recipe = mRecipes.get(position);
            holder.bind(recipe);
        }

        /**
         * Number of recipes.
         * @return int.
         */
        @Override
        public int getItemCount() {
            return mRecipes.size();//number of recipes
        }

        /**
         * Update the RecipeListFragment.
         * @param recipes Collection of Recipes.
         */
        void setRecipes(List<Recipe> recipes){
            mRecipes = recipes;
        }
    }
    //endregion

}
