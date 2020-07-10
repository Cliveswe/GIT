package model;

import android.annotation.SuppressLint;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.UUID;

/**
 * A recipe class the holds the name of the recipe a list of ingredients and instructions on how
 * to prepare the recipe.
 */
public class Recipe {
    private final UUID mId;
    private String mName;
    private Date mDate;
    private String mIngredients;//list of ingredients
    private String mInstructions;//preparation instructions
    private String mGuest;//invite a person to dinner
    private String mCategory;//recipe mCategory

    /**
     * Class constructor.
     */
    public Recipe(){
        this(UUID.randomUUID());
    }

    /**
     * Class constructor.
     * @param id UUID a unique random number.
     */
    public Recipe(UUID id){
        mId = id;
        mDate = new Date();
        mName = "";
        mIngredients = "";
        mInstructions = "";
        mCategory = "";
    }

    /**
     * Unique identification for the recipe.
     * @return unique random number.
     */
    public UUID getId() {
        return mId;
    }

    /**
     * Name of the recipe.
     * @return string name.
     */
    public String getName() {
        return mName;
    }

    /**
     * Change the name of a recipe.
     * @param name a string.
     */
    public void setName(String name) {
        mName = name;
    }

    /**
     * Get the list of ingredients.
     * @return list of ingredients as a string.
     */
    public String getIngredients() {
        return mIngredients;
    }

    /**
     * Add a list of ingredients.
     * @param ingredients list of ingredients as a string.
     */
    public void setIngredients(String ingredients) {
        mIngredients = ingredients;
    }


    /**
     * Get the instruction on how to prepare the recipe.
     * @return instructions as a string.
     */
    public String getInstructions() {
        return mInstructions;
    }

    /**
     * Add instructions to the recipe.
     * @param directions instructions as a string.
     */
    public void setInstructions(String directions) {
        mInstructions = directions;
    }

    /**
     * A date associated with the recipe.
     * @return a date object.
     */
    public Date getDate() {
        return mDate;
    }

    /**
     * Get the date as a formatted string. The format is day in week, day in month, month in year
     * and year.
     * @return simple date as string "EEE dd MMM yyyy"
     */
    @SuppressLint("SimpleDateFormat")
    public String getDateAsFormattedString(){
        SimpleDateFormat formatter;
        String result;

        formatter = new SimpleDateFormat("EEEE dd MMM yyyy");
        result = formatter.format(mDate);

        return result;
    }

    /**
     * Update the date of the recipe.
     * @param date a date object.
     */
    public void setDate(Date date) {
        mDate = date;
    }

    /**
     * Who was invited to dinner?
     * @return A person as String.
     */
    public String getGuest() {
        return mGuest;
    }

    /**
     * Set a invited guest.
     * @param guest string
     */
    public void setGuest(String guest) {
        mGuest = guest;
    }

    /**
     * Design the photo file name.
     * @return the name of the file as String
     */
    public String getPhotoFilename(){
        return "IMG_" + getId().toString() + ".jpg";
    }

    /**
     * Get the recipes mCategory.
     * @return Category as String.
     */
    public String getCategory() {
        return mCategory;
    }

    /**
     * Set the recipes mCategory.
     * @param category a recipe category.
     */
    public void setCategory(String category) {
        this.mCategory = category;
    }


}
