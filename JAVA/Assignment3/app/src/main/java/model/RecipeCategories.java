package model;

import java.util.HashMap;
import java.util.Map;

public enum RecipeCategories {
    NONE("Choose category"),
    FISH("Fish"),
    MEAT("Meat"),
    SALAD ("Salad"),
    SEAFOOD("Seafood"),
    VEGETARIAN("Vegetarian");

    private String recipeCategoryByName;

    RecipeCategories(String recipeCategoryByName){
        this.recipeCategoryByName = recipeCategoryByName;
    }

    @SuppressWarnings("unused")
    public boolean equals(String otherName) {
        return recipeCategoryByName.equals(otherName);
    }


    private static final Map<String, RecipeCategories> lookup =
            new HashMap<>();
    static {
        for (RecipeCategories element : RecipeCategories.values()) {
            lookup.put(element.toString(), element);
        }
    }

    public static RecipeCategories elementOf(String element) {
        return lookup.get(element);
    }


    @SuppressWarnings("WeakerAccess")
    @Override
    public String toString(){
        return recipeCategoryByName;
    }
}
