package Model;

import android.os.Parcel;
import android.os.Parcelable;
import java.util.List;

/**
 * Created by Clive Leddy on 2018-02-10.
 */

public class ThirtyGameManager implements Parcelable{
    private int numberOfTurnsPerGame;
    private int gameRounds;
    private Player player;//a player
    private int numberOfPlayerTurns;
    private Dice dice;//a group of die
    private TallyDie tallyDie;//tally dice result for player
    private int tallyDiceLowScoreCalculationBoundary;
    private boolean gameStatus;
    private boolean playerStatus;


    public ThirtyGameManager(){
        numberOfTurnsPerGame = 10;
        numberOfPlayerTurns = 3;
        tallyDiceLowScoreCalculationBoundary = 3;
        Initialise();
    }

    /**
     * Initialise the class.
     * */
    private void Initialise(){
        player = new Player(this.numberOfPlayerTurns);//new player
        dice = new Dice();//new set of Dice
        tallyDie = new TallyDie(0);
        gameStarted();
        setGameRounds(0);
    }

    /**
     * What is the LOW score calculation boundary.
     * @return the lowest Die max value used in calculating LOW result.
     */
    private int getTallyLowBoundary(){
        return tallyDiceLowScoreCalculationBoundary;
    }

    private void gameStarted(){
        setGameStatus(true);
        setPlayerStatus(true);
    }

    private void setGameStatus(boolean status){
        gameStatus = status;
    }

    public boolean getGameStatus(){
        return  gameStatus;
    }

    private void setPlayerStatus(boolean status){
        playerStatus = status;
    }

    private boolean getPlayerStatus(){
        return playerStatus;
    }

    private void setGameRounds(int i){
        gameRounds = i;
    }

    public int getGameRounds(){return  gameRounds;}

    private void updateGameStatus(){
        if(getGameRounds() >= (numberOfTurnsPerGame - 1)){
            setGameStatus(false);
        }
    }

    /**
     * Retrieve the players complete list of scores.
     * @return A list of score results per round.
     */
    public List<Integer> getAllScoresForPlayer(){
        return player.getScoresPerTurn();
    }

    public int getTotalScoreForGame(){
        return player.getTotalScore();
    }

    public int getScoreForRound(){
        return tallyDie.getBestScore();
    }
    /**
     * Get the value of a selected die from a group of dice.
     * @param id the chosen die.
     * @return the face value of the die.
     * */
    public int getDieValue(int id){
        Die d = dice.getDie(id);
        return d.getValue();
    }

    /**
     * Tally the dice score.
     * */
    public void tallyDiceScore(){
        Dice gameDice = new Dice(dice);//make a copy of the dice to examine
        //calculate the dice tally
        tallyDie = new TallyDie(getTallyLowBoundary());//create the tally dice logic
        tallyDie.tallyCalculate(gameDice);//add the dice to be calculated
        //update the player
        player.addScore(tallyDie.getBestScore());
        //update the game
        updateGameStatus();
        nextRound();
    }

    /**
     * Has a die been selected?
     * @return true if at least one die has been selected otherwise false.
     * */
    public boolean isDiceSelected(){
        return dice.hasADieBeenSelected();
    }

    /**
     * Roll an unselected die.
     * @param index a die to roll.
     * */
    public void rollDice(int index) {
        dice.rollDie(index);
    }

    /**
     * Get the number of die used in this game.
     * */
    public int numOfDiceThisGame(){
        return dice.getNumberOfDie();
    }

    /**
     * A player took a turn
     * */
    public void playerRolledDice(){
        player.takeTurn();
    }

    public boolean canRollDiceForPlayer(){
        return player.canTakeTurn();
    }

    /**
     * Check if a die has been selected.
     * */
    public boolean isDiceSelected(int index){
        return dice.isSelectDie(index);
    }

    /**
     * A Die has been selected.
     * @param index a specific Die has been selected.
     */
    public void select(int index) {
        dice.selectDie(index);
    }

    /**
     * A new round has begun.
     */
    private void nextRound(){

        resetPlayerTurns();//player has new turns
        dice.unselectAllDieInDice();//reset dice a set of Die
        setGameRounds(getGameRounds() + 1);
    }

    /**
     * A player can roll the Dice again.
     */
    private void resetPlayerTurns(){
        player.newTurn();
    }

    /**
     * A Die has been unselected.
     * @param index a specific Die to be unselected.
     */
    public void unSelect(int index) {
        dice.unSelectDie(index);
    }


    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeInt(numberOfTurnsPerGame);
        dest.writeInt(gameRounds);
        dest.writeParcelable(player, flags);
        dest.writeParcelable(dice, flags);
        dest.writeParcelable(tallyDie, flags);
        dest.writeInt(tallyDiceLowScoreCalculationBoundary);
        dest.writeByte(gameStatus ? (byte) 1 : (byte) 0);
        dest.writeByte(playerStatus ? (byte) 1 : (byte) 0);

    }

    public ThirtyGameManager(Parcel in) {
        this.numberOfTurnsPerGame = in.readInt();
        this.gameRounds = in.readInt();
        this.player = in.readParcelable(getClass().getClassLoader());
        this.dice = in.readParcelable(getClass().getClassLoader());
        this.tallyDie = in.readParcelable(getClass().getClassLoader());
        this.tallyDiceLowScoreCalculationBoundary = in.readInt();
        this.gameStatus = in.readByte() != 0;
        this.playerStatus = in.readByte() != 0;
    }

    public static final Parcelable.Creator<ThirtyGameManager> CREATOR = new Parcelable.Creator<ThirtyGameManager>() {
        @Override
        public ThirtyGameManager createFromParcel(Parcel source) {
            return new ThirtyGameManager(source);
        }

        @Override
        public ThirtyGameManager[] newArray(int size) {
            return new ThirtyGameManager[size];
        }
    };
}
