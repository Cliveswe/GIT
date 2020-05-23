package com.cliveleddy.mycookbook.database;

import android.database.Cursor;
import android.database.CursorWrapper;

import java.util.Date;
import java.util.UUID;

import model.Recipe;

import static com.cliveleddy.mycookbook.database.RecipeDbSchema.*;

public class RecipeCursorWrapper extends CursorWrapper {
    /**
     * Create a wrapper around a Cursor. This class now contains all the methods as the cursor it
     * wraps.
     * @param cursor Cursor.
     */
    public RecipeCursorWrapper(Cursor cursor){
        super(cursor);
    }

    /**
     * Pulling raw data out of a cursor.
     * @return Recipe
     */
    public Recipe getRecipe(){
        String uuidString = getString(getColumnIndex(RecipeTable.Cols.UUID));
        String name = getString(getColumnIndex(RecipeTable.Cols.NAME));
        long date = getLong(getColumnIndex(RecipeTable.Cols.DATE));
        String ingredientsString = getString(getColumnIndex(RecipeTable.Cols.INGREDIENTS));
        String instructionsString = getString(getColumnIndex(RecipeTable.Cols.INSTRUCTIONS));
        String guestString = getString(getColumnIndex(RecipeTable.Cols.GUEST));
        String categoryString = getString(getColumnIndex(RecipeTable.Cols.CATEGORY));

        Recipe recipe = new Recipe(UUID.fromString(uuidString));
        recipe.setName(name);
        recipe.setDate(new Date(date));
        recipe.setIngredients(ingredientsString);
        recipe.setInstructions(instructionsString);
        recipe.setGuest(guestString);
        recipe.setCategory(categoryString);
        return recipe;
    }
}
