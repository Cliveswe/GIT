package com.cliveleddy.mycookbook;

import android.support.v4.app.Fragment;

public class RecipeListActivity extends SingleFragmentActivity {

    /**
     * Implement the SingleFragmentActivity method createFragment.
     * @return an instance of the recipe list fragment that the activity is hosting.
     */
    @Override
    protected Fragment createFragment() {
        return new RecipeListFragment();
    }

}
