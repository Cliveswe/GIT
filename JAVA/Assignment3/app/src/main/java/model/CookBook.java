package model;

import android.annotation.SuppressLint;
import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.cliveleddy.mycookbook.database.RecipeBaseHelper;
import com.cliveleddy.mycookbook.database.RecipeCursorWrapper;

import java.io.File;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import static com.cliveleddy.mycookbook.database.RecipeDbSchema.*;

/**
 * CookBook that contains a list of recipes as a singleton class.
 * This object will exists in memory as long as the application stays in memory.
 * Temporary solution until persistent storage with SQLite Db.
 */
public class CookBook {

    @SuppressLint("StaticFieldLeak")
    private static CookBook sCookBook = null;
    private  final Context mContext;
    private final SQLiteDatabase mDatabase;

    /**
     * Private class constructor.
     * @param context object context.
     */
    private CookBook(Context context) {
        mContext = context;
        //use RecipeBaseHelper class is used to create the Db
        mDatabase = new RecipeBaseHelper(mContext)
                .getWritableDatabase();
    }

    /**
     * If an existence of the singleton class exists then get it or else create it.
     * @param context object context.
     * @return the singleton object.
     */
    public static CookBook get(Context context){
        if(sCookBook == null){
            sCookBook = new CookBook(context.getApplicationContext());
        }
        return sCookBook;
    }

    /**
     * Add a recipe to the cookbook.
     * @param recipe a recipe.
     */
    public void addRecipe(Recipe recipe){
        //mRecipes.add(recipe);
        ContentValues values = getContentValues(recipe);
        mDatabase.insert(RecipeTable.NAME, null, values);
    }

    /**
     * Delete a recipe from the cookbook.
     * @param recipe a recipe
     */
    public void deleteRecipe(Recipe recipe){
        String whereClause = RecipeTable.Cols.UUID + " = ?";
        String [] whereArgs = new String[]{recipe.getId().toString()};

        mDatabase.delete(RecipeTable.NAME, whereClause, whereArgs);
    }


    /**
     * Get the list of recipes.
     * @return a reference to a collection of recipes.
     */
    public List<Recipe> getRecipes() {
        List<Recipe> recipes = new ArrayList<>();

        //position the cursor in the Db
        try (RecipeCursorWrapper cursor = queryRecipes(null, null)) {
            //move the cursor to the first element in the Db
            cursor.moveToFirst();
            //read each row of data in the Db until end of data set
            while (!cursor.isAfterLast()) {
                recipes.add(cursor.getRecipe());
                cursor.moveToNext();//advance to the next row
            }
        }
        //return mRecipes;
        return recipes;
    }

    /**
     * Get a recipe.
     * @param uuid UUID of a recipe.
     * @return recipe object reference.
     */
    public Recipe getRecipe(UUID uuid){
        //query the Db
        try (RecipeCursorWrapper cursor = queryRecipes(
                RecipeTable.Cols.UUID + " = ?",
                new String[]{uuid.toString()}
        )) {
            if (cursor.getCount() == 0) {
                return null;
            }
            cursor.moveToFirst();//reposition the cursor
            return cursor.getRecipe();//retrieve the recipe
        }
    }

    /**
     * Location of photo.
     * @param recipe a recipe.
     * @return file objects location.
     */
    public File getPhotoFile(Recipe recipe){
        File filesDir = mContext.getFilesDir();
        return new File(filesDir, recipe.getPhotoFilename());
    }

    /**
     * Update the cookbook with a recipe.
     * @param recipe
     */
    public void updateRecipe(Recipe recipe){
        String uuidString = recipe.getId().toString();
        ContentValues values = getContentValues(recipe);
        String whereClause = RecipeTable.Cols.UUID + " = ?";
        String [] whereArgs = new String[]{uuidString};

        mDatabase.update(RecipeTable.NAME, values, whereClause, whereArgs);
    }

    /**
     * Querying for recipes.
     * @param whereClause
     * @param whereArgs
     * @return RecipeCursorWrapper a wrapped cursor.
     */
    private RecipeCursorWrapper queryRecipes(String whereClause, String[] whereArgs){
        @SuppressLint("Recycle") Cursor cursor = mDatabase.query(
                RecipeTable.NAME,
                null,//select all columns
                whereClause,
                whereArgs,
                null,
                null,
                null

        );
        return new RecipeCursorWrapper(cursor);
    }

    /**
     * Writes and updates to the Db are done with the assistance of this class.
     * @param recipe Recipe object
     * @return
     */
    private static ContentValues getContentValues(Recipe recipe){
        ContentValues values = new ContentValues();

        values.put(RecipeTable.Cols.UUID, recipe.getId().toString());
        values.put(RecipeTable.Cols.NAME, recipe.getName());
        values.put(RecipeTable.Cols.DATE, recipe.getDate().getTime());
        values.put(RecipeTable.Cols.INGREDIENTS, recipe.getIngredients());
        values.put(RecipeTable.Cols.INSTRUCTIONS, recipe.getInstructions());
        values.put(RecipeTable.Cols.GUEST, recipe.getGuest());
        values.put(RecipeTable.Cols.CATEGORY, recipe.getCategory());

        return values;
    }
}
