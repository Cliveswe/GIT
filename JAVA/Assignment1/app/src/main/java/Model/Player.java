package Model;

import android.annotation.SuppressLint;
import android.os.Parcel;
import android.os.Parcelable;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by Clive Leddy on 2018-02-10.
 */

public class Player implements Parcelable {
    private int turns;//number of turns remaining
    private int maxTurns;

    private List<Integer> pointsPerTurn;

    /**
     * Class constructor.
     * @param maxTurns How many turns is a player allowed?
     */
    public Player(int maxTurns){

        this.maxTurns = maxTurns;
        initialize();
    }

    /**
     * Get the number of turns.
     * @return number of turns remaining.
     * */
    private int getTurns() {
        return turns;
    }

    /**
     * Set the number of turns the player has.
     * @param turn number of turns.
     * */
    private void setTurns(int turn) {
        this.turns = turn;
    }

    /**
     * Get the players total points.
     * @return points total.
     * */
    public int getTotalScore() {
        int res = 0;
        for (Integer elements: pointsPerTurn){
            res = res + elements;
        }
        return res;
    }

    /**
     * Get the players score per turn.
     * @return a list of scores.
     * */
    public List<Integer> getScoresPerTurn() {
        List<Integer> copyPointPerTurn = new ArrayList<>();
        copyPointPerTurn.addAll(this.pointsPerTurn);
        return copyPointPerTurn;
    }

    /**
     * Set the total points per player turn.
     * @param turnPoints the total number of points on this turn.
     * */
    public void addScore(int turnPoints) {

        pointsPerTurn.add(turnPoints);
    }

    /**
     * Player takes a new turn.
     * */
    public void newTurn(){
        setTurns(maxTurns);
    }

    /**
     * Player takes a turn.
     * */
    public void takeTurn(){
        setTurns(turns - 1);
    }

    /**
     * Is a player allowed to take a turn?
     * @return True if the player is allow to take a turn, false otherwise.
     * */
    public boolean canTakeTurn(){
        boolean ans = false;
        if((getTurns() > 0) && (getTurns() <= maxTurns)){
            ans = true;
        }
        return ans;
    }

    /**
     * Initialize the field variables.
     * */
    private void initialize(){
        pointsPerTurn = new ArrayList<>();
        turns = maxTurns;
    }


    @SuppressLint("DefaultLocale")
    @Override
    public String toString(){
        StringBuilder allScores = new StringBuilder();
        //Title
        allScores.append("Score per round (" + pointsPerTurn.size() +") :\n");

        for (Integer element: pointsPerTurn) {//for each round
            //add the score to the output
            allScores.append(String.format("%-3d %-3d\n",
                    pointsPerTurn.indexOf(element),  element));
        }

        return allScores.toString();
    }


    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {

        dest.writeInt(turns);
        dest.writeInt(maxTurns);
        dest.writeList(pointsPerTurn);
    }

    protected Player(Parcel in) {

        this.turns = in.readInt();
        this.maxTurns = in.readInt();
        this.pointsPerTurn = in.readArrayList(null);

    }

    public static final Creator<Player> CREATOR = new Creator<Player>() {
        @Override
        public Player createFromParcel(Parcel source) {
            return new Player(source);
        }

        @Override
        public Player[] newArray(int size) {
            return new Player[size];
        }
    };
}
