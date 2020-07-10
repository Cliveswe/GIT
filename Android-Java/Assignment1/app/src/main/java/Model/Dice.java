package Model;

import android.os.Parcel;
import android.os.Parcelable;
import java.util.HashMap;
import java.util.Map;

/**
 * Created by Clive Leddy on 2018-02-10.
 */

public class Dice implements  Parcelable{
    private int min = 1;
    private int max = 6;
    //There are 6 die in the game
    private Map<Integer, Die> mDice;

    /**
     * Class constructor.
     * */
    public Dice(){
        Initialize();
    }

    /**
     * Copy constructor.
     * */
    public Dice(Dice other){
        mDice = new HashMap();

        //make a copy form other Die
        for(int i = min; i <= max; i++){
            this.mDice.put(i, other.getDie(i));
        }
    }

    /**
     * Examine all die and confirm that at least one die has been selected.
     * @return returns true if a die has been selected otherwise false
     * */
    public boolean hasADieBeenSelected(){
        for (int i = min; i <= max; i++){
            if(isSelectDie(i))
                return true;
        }
        return false;
    }

    /**
     * The number of dice to play with.
     * @return The total number of dice.
     * */
    public int getNumberOfDie(){
        return max;
    }
    /**
     * Get a copy of the chosen die.
     * */
    public Die getDie(int index){
        Die tmp;
        tmp = new Die(mDice.get(index));
        return tmp;
    }

    /**
     * Select a die.
     * @param index the die to be selected
     * */
    public void selectDie(int index){
        mDice.get(index).selectDie();
    }

    /**
     * Select a die.
     * @param index the die to be selected
     * */
    public void unSelectDie(int index){
        mDice.get(index).unSelectDie();
    }

    /**
     * Has a die already been select.
     * @param index the die to be selected
     * */
    public boolean isSelectDie(int index){
        return mDice.get(index).isSelect();
    }

    /**
     * Roll a die.
     * @param index roll a chosen die.
     * */
    public void rollDie(int index){
        mDice.get(index).rollDie();
    }

    /**
     * Initialize all the dice
     * */
    private void Initialize() {
        mDice = new HashMap();

        for(int i = min; i <= max; i++){
            mDice.put(i, new Die());
        }
    }

    /**
     * Unselect all Die in mDice
     */
    public void unselectAllDieInDice(){
        for(int index = min; index <= max; index++){
            unSelectDie(index);
        }
    }

    @Override
    public String toString(){
        StringBuilder res = new StringBuilder();

        for (int i = min; i <= max; i++){
            res.append("Die " + i + " " + mDice.get(i) + "\n");
        }
        return res.toString();
    }


    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeInt(min);
        dest.writeInt(max);
        dest.writeMap(this.mDice);
    }

    protected Dice(Parcel in) {
        this.min = in.readInt();
        this.max = in.readInt();
        this.mDice = in.readHashMap(getClass().getClassLoader());//Die.class.getClassLoader is the object stored in the Map
    }

    public static final Parcelable.Creator<Dice> CREATOR = new Parcelable.Creator<Dice>() {
        @Override
        public Dice createFromParcel(Parcel source) {
            return new Dice(source);
        }

        @Override
        public Dice[] newArray(int size) {
            return new Dice[size];
        }
    };
}

