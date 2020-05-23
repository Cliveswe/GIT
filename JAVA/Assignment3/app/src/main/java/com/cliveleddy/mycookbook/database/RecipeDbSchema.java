package com.cliveleddy.mycookbook.database;

public class RecipeDbSchema {
    /**
     * RecipeTable describes the Db table.
     */
    public static final class RecipeTable {
        //Db description
        public static final String NAME = "recipes";

        /**
         * Define the columns of the table.
         */
        public static final class Cols {
            public static final String UUID = "uuid";
            public static final String NAME = "name";
            public static final String DATE = "date";
            public static final String INGREDIENTS = "ingredients";
            public static final String INSTRUCTIONS = "instructions";
            public static final String GUEST = "guest";
            public static final String CATEGORY = "category";
        }
    }
}