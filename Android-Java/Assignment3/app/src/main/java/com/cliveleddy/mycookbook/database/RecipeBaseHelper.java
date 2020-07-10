package com.cliveleddy.mycookbook.database;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.cliveleddy.mycookbook.database.RecipeDbSchema.RecipeTable;

public class RecipeBaseHelper extends SQLiteOpenHelper {
    private static final int VERSION = 1;
    private static final String DATABASE_NAME = "recipeBase.db";

    public RecipeBaseHelper(Context context){
        super(context, DATABASE_NAME, null, VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        db.execSQL("create table " + RecipeTable.NAME + "(" +
                " _id integer primary key autoincrement, " +
                RecipeTable.Cols.UUID + ", " +
                RecipeTable.Cols.NAME + ", " +
                RecipeTable.Cols.DATE + ", " +
                RecipeTable.Cols.INGREDIENTS + ", " +
                RecipeTable.Cols.INSTRUCTIONS + ", " +
                RecipeTable.Cols.GUEST + ", " +
                RecipeTable.Cols.CATEGORY +
                ")"
        );
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        //not implemented
    }
}
