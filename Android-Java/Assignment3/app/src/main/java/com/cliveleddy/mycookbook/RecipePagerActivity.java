package com.cliveleddy.mycookbook;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentStatePagerAdapter;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;

import java.util.List;
import java.util.UUID;

import model.CookBook;
import model.Recipe;

public class RecipePagerActivity extends AppCompatActivity {
    private static final String EXTRA_RECIPE_ID = "com.cliveleddy.mycookbook.recipe_id";
    @SuppressWarnings("FieldCanBeLocal")
    private ViewPager mViewPager;
    private List<Recipe> mRecipes;

    public static Intent newIntent(Context packageContext, UUID recipeId){
        //create an explicit intent
        Intent intent = new Intent(packageContext, RecipePagerActivity.class);
        //pass the intent as a key value that maps to a recipe
        intent.putExtra(EXTRA_RECIPE_ID, recipeId);
        return intent;
    }


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recipe_pager);

        UUID recipeId = (UUID) getIntent()
                .getSerializableExtra(EXTRA_RECIPE_ID);

        mViewPager = findViewById(R.id.recipe_view_pager);//locate the ViewPager

        mRecipes = CookBook.get(this).getRecipes();//get a list of recipes from the cookbook

        FragmentManager fragmentManager = getSupportFragmentManager();//get activity instance
        //set the adapter
        mViewPager.setAdapter(new FragmentStatePagerAdapter(fragmentManager) {
            @Override
            public int getCount() {
                return mRecipes.size();//number of items in the list of recipes
            }

            @Override
            public Fragment getItem(int position) {
                //fetch a recipe instance for a given position
                Recipe recipe = mRecipes.get(position);
                //create and return a properly configured RecipeFragment
                return RecipeFragment.newInstance(recipe.getId());
            }
        });

        //find the index of the recipe to display
        for (int i = 0; i < mRecipes.size(); i++){
            if(mRecipes.get(i).getId().equals(recipeId)){
                mViewPager.setCurrentItem(i);
                break;
            }
        }
    }
}
