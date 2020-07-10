package Model;

import android.os.Parcel;
import android.os.Parcelable;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

/**
 * Created by Clive Leddy on 2018-02-13.
 */

public class TallyDie implements Parcelable{
    private int lowDieValue;
    private Dice dice;//a players dice.
    private int score;//a players score.
    private int lowScore;//low score total.

    //A list of numbered sets, to be used as set operations in Set Theory (a branch of mathematical logic)
    private List<Set<Integer>> result = new ArrayList<>();

    /**
     * Class constructor
     * @param lowDieValue the lowest value a die has to be included in the LOW calculations.
     */
    public TallyDie(int lowDieValue)  {
        this.lowDieValue = lowDieValue;
        score = 0;
        lowScore = 0;
        //   result.add(new ArrayList<Integer>());
        result.add(new HashSet<>());
    }

    /**
     * Set the dice to be calculated.
     *
     * @param dice a players dice.
     */
    private void setDice(Dice dice) {
        this.dice = dice;
    }

    /**
     * Save a copy of the dice and calculate the results.
     *
     * @param dice a players dice.
     */
    public void tallyCalculate(Dice dice) {
        setDice(dice);
        calculateLowScore();
        diceConvertedToSets();
        calculateScore();
        printNewResult();
    }

    /**
     * Get the calculated score for the Dice.
     *
     * @return Dice score.
     */
    public int getBestScore() {
        if (getScore() > getLowScore()) {
            return getScore();
        }
        return getLowScore();
    }

    /**
     * Convert a list of Die into a new list of Integer Sets
     */
    private void diceConvertedToSets() {
        sortDiceIntoSets();
        intersectSets();
        checkDieCombinations();
    }

    /**
     * Sum the value of each Set member to the variable score
     */
    private void calculateScore() {
        if(result.size()> 1) {
            for (Set<Integer> set : result) {//for each Set in the result list
                for (Integer intInSet : set) {//for each int in a Set
                    addToScore(intInSet);//sum the value to the score
                }
            }
        }else{
            calculateLowScore();
        }
    }

    /**
     * Calculate the LOW score.
     */
    private void calculateLowScore() {
        int value;
        boolean isSelected;

        for (int index = 1; index <= dice.getNumberOfDie(); index++) {//for each Die
            value = dice.getDie(index).getValue();//get the Die value
            isSelected = dice.getDie(index).isSelect();//get Die status
            if ((value <= lowDieValue) && (isSelected)) {
                addToLowScore(value);//sum the Die value
            }
        }
    }

    /**
     * Update the current low score by adding a new value.
     *
     * @param v the value to add to the low score.
     */
    private void addToLowScore(int v) {
        setLowScore(getLowScore() + v);
    }

    /**
     * Update the current score by adding a new value.
     * @param v the value to add to the score.
     */
    private void addToScore(int v) {
        setScore(getScore() + v);
    }

    /**
     * Check a set of die in the list of dice that are equal.
     * Remove any or all set of die that are not equal.
     */
    private void checkDieCombinations() {
        int keySize;
        List<Set<Integer>> tmp;

        //there are more than one sets
        if (result.size() > 1) {
            keySize = result.get(1).size();//get the size of the key set
            tmp = checkDieCombinationsResult(keySize);
            result.clear();//remove all the Sets in the result list
            result.addAll(tmp);//add the result from the checked Die combinations
        }
    }

    /**
     * Using a key create a new temporary result containing the sets that match the key.
     * @param keySize The key to compare each Set with.
     * @return A new list of Sets.
     */
    private List<Set<Integer>> checkDieCombinationsResult(int keySize){
        List<Set<Integer>> tmp = new ArrayList<>();

        for (Set<Integer> element: result) {
            if(element.size() == keySize){
                tmp.add(element);
            }
        }
        return tmp;
    }

    /**
     * Get the calculated low score
     *
     * @return The total sum of all die values that are 3 or less.
     */
    private int getLowScore() {
        return lowScore;
    }

    /**
     * Set the calculated low score.
     *
     * @param value the score from the Dice.
     */
    private void setLowScore(int value) {
        lowScore = value;
    }

    /**
     * Get the calculated score
     *
     * @return The score from the Dice.
     */
    private int getScore() {
        return score;
    }

    /**
     * Set the calculated score.
     *
     * @param value the score from the Dice.
     */
    private void setScore(int value) {
        score = value;
    }

    /**
     * Retain all elements of a set that are equal to the key.
     */
    private void intersectSets() {
        if (result.size() > 1) {
            Set<Integer> keySet = result.get(1);//key set use in intersection of sets
            for (Set<Integer> element : result) {
                element.retainAll(keySet);//retain elements that are in key set
            }
        }
    }

    /**
     * Print the current status of the class result list, low and score values.
     */
    private void printNewResult() {
        for (Set<Integer> element : result) {
            System.out.println("result " + element.toString());
        }
    }

    /**
     * Amend a Die's value into a Set.
     */
    private void sortDiceIntoSets() {
        for (int i = 1; i <= dice.getNumberOfDie(); i++) {
            if (dice.isSelectDie(i)) {
                logDieToResult(dice.getDie(i));
                dice.unSelectDie(i);
            }
        }
    }

    /**
     * Add the value of a Die to a list of integers as Sets
     *
     * @param d A Die that has a value and status.
     */
    private void logDieToResult(Die d) {
        int index = 0;
        boolean addDieSuccess;

        do {//for every Set in the result list
            Set<Integer> getSetFromList = result.get(index);//get a Set
            addDieSuccess = getSetFromList.add(d.getValue());//amend a Die value to the Set
            index++;
        }
        while (!addDieSuccess && (index < result.size()));//could not add a value to Set or reached end of list

        if (!addDieSuccess) {//could not add Die value to Set
            Set<Integer> newSet = new HashSet<>();//create a new Set
            newSet.add(d.getValue());//add the Die value to the Set
            result.add(newSet);//add the Set to the list of Set's
        }
    }

    /**
     * Add a die value to the list if unique.
     *
     * @param e a list of integers.
     * @param d a selected die with value.
     * @return true if the die value was added to the list false otherwise.
     */
    private boolean addDieValue(ArrayList<Integer> e, Die d) {
        //the die value is not in the list
        if (!e.contains(d.getValue())) {
            return true;
        }
        return false;
    }

    @Override
    public String toString(){
        String textLowScore;
        String textScore;

        if (getLowScore() >= 0) {
            textLowScore = "LOW: " + getLowScore();
        } else {
            textLowScore = "LOW: N/A";
        }

        if (this.getScore() >= 0) {
            textScore = "Calculated: " + this.getScore();
        } else {
            textScore = "Calculated: N/A";
        }

        return "Score: \n" + textLowScore + "\n" + textScore;

    }

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {

        dest.writeInt(lowDieValue);
        dest.writeParcelable(dice, flags);
        dest.writeInt(score);
        dest.writeInt(lowScore);
        dest.writeList(result);
    }

    protected TallyDie(Parcel in) {

        this.lowDieValue = in.readInt();
        this.dice = in.readParcelable(getClass().getClassLoader());
        this.score = in.readInt();
        this.lowScore = in.readInt();
        this.result = in.readArrayList(null);
    }

    public static final Parcelable.Creator<TallyDie> CREATOR = new Parcelable.Creator<TallyDie>() {
        @Override
        public TallyDie createFromParcel(Parcel source) {
            return new TallyDie(source);
        }

        @Override
        public TallyDie[] newArray(int size) {
            return new TallyDie[size];
        }
    };
}
