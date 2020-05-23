package Model;

import android.os.Parcel;
import android.os.Parcelable;
import java.util.Random;

/**
 * Created by Clive Leddy on 2018-02-10.
 * Roll a die to get its value.
 */

public class Die implements  Parcelable{
    private int value;
    private boolean select;
    private int min = 1;
    private int max = 6;

    /**
     * Create a die.
     * */
    public Die() {
        unSelectDie();
        rollDie();
    }

    /**
     * Copy constructor.
     * */
    public Die (Die other){

        this.value = other.value;
        this.select = other.select;
    }

    /**
     * Die has been selected
     * */
    public void selectDie(){
        select = true;
    }

    /**
     * Die has been selected
     * */
    public void unSelectDie(){
        select = false;
    }

    /**
     * Has the die been selected.
     * */
    public boolean isSelect(){
        return select;
    }

    /**
     * Get the value of the die.
     * @return die value.
     * */
    public int getValue(){
        return value;
    }

    /**
     * Set the field value.
     * @param value die value.
     * */
    private void setValue(int value){
        this.value = value;
    }
    /**
     * Randomly choose a value in the range from 1 to 6.
     * */
    public void rollDie(){
        if(!isSelect()) {
            Random r = new Random();

            //get a value from min (inclusive) to max (inclusive)
            setValue(r.nextInt((max - min) + min) + min);
        }
    }

    @Override
    public String toString(){
        return "Selected " + isSelect() + " value " + getValue();
    }


    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {

        dest.writeInt(value);
        dest.writeByte(select ? (byte) 1 : (byte) 0);
        dest.writeInt(min);
        dest.writeInt(max);
    }

    protected Die(Parcel in) {

        this.value = in.readInt();
        this.select = in.readByte() != 0;
        this.min = in.readInt();
        this.max = in.readInt();

    }

    public static final Parcelable.Creator<Die> CREATOR = new Parcelable.Creator<Die>() {
        @Override
        public Die createFromParcel(Parcel source) {
            return new Die(source);
        }

        @Override
        public Die[] newArray(int size) {
            return new Die[size];
        }
    };
}
