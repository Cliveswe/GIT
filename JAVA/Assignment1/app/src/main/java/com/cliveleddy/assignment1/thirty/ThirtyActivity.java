package com.cliveleddy.assignment1.thirty;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;
import java.util.ArrayList;
import java.util.List;
import Model.ColourConstants;
import Model.ThirtyGameManager;

public class ThirtyActivity extends Activity{
    private static final boolean LONG = true;
    private static final boolean YES = true;
    private static final boolean NO = false;
    private boolean dicedRolled;

    private ThirtyGameManager thirtyGameManager;//game manager
    private TextView runningResultText; //Show running results
    //buttons
    private List<ImageButton> buttonList;
    private Button rollDiceButton;
    private Button calculateDiceButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.d(getString(R.string.TAG), getString(R.string.onCreateBundleCalled));

        setContentView(R.layout.activity_thirty);
        buttonList = new ArrayList<>();//list of die image buttons
        runningResultText = findViewById(R.id.game_status);
        //find button id
        rollDiceButton = findViewById(R.id.roll_dice);//identify the roll dice button
        calculateDiceButton = findViewById(R.id.calculate_dice);//identify the calculate dice button


        if(savedInstanceState != null){//reuse game instance
            runningResultText.setText(savedInstanceState.getString(getString(R.string.KEY_RUNNINGRESULTTEXT)));
            thirtyGameManager  = savedInstanceState.getParcelable(getString(R.string.KEY_THIRTYGAMEMANAGER));
            recover();//update button from saved data
        }else {//new game
            thirtyGameManager = new ThirtyGameManager();
            Initialise();
        }
    }

    /**
     * If the game has ended show the results in a new activity.
     */
    private void showGameResults(){
        Intent resultIntent;//pass data between activities
        int requestCode = 100;//use to communicate between activities
        Log.d(getString(R.string.TAG), getString(R.string.showGameResults));

        if(thirtyGameManager.getGameStatus() == false){
            //connect activities
            resultIntent = new Intent(ThirtyActivity.this, ThirtyResultsActivity.class);

            //add a copy of the game manager to the results activity
            resultIntent.putExtra(getString(R.string.ThirtyGameManager),thirtyGameManager);
            //Start result activity
            startActivityForResult(resultIntent, requestCode);
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data){
        super.onActivityResult(requestCode,resultCode, data);

        if(resultCode < 0){
            thirtyGameManager = new ThirtyGameManager();
            buttonList = new ArrayList<>();//list of die image buttons
            Initialise();
        }else {
            finish();
            android.os.Process.killProcess(android.os.Process.myPid());
            System.exit(0);

        }
    }

    @Override
    public  void onSaveInstanceState(Bundle savedInstanceState){
        super.onSaveInstanceState(savedInstanceState);
        Log.d(getString(R.string.TAG), getString(R.string.onSaveInstanceState));
        savedInstanceState.setClassLoader(getClass().getClassLoader());
        //store the game manager

        savedInstanceState.putString(getString(R.string.KEY_RUNNINGRESULTTEXT), runningResultText.getText().toString());
        savedInstanceState.putParcelable(getString(R.string.KEY_THIRTYGAMEMANAGER), thirtyGameManager);
    }

    /**
     * Recover and reset both Dice status and values.
     */
    private void recover() {
        Log.i(getString(R.string.TAG), getString(R.string.recover));
        Initialise();//reset buttons
        recoverButtonData();//update button data
        if (thirtyGameManager.getGameRounds() > 0){
            printCurrentResults();//update status
        } else {
            printStartUpMessage();
        }

    }

    private void recoverButtonData(){
        Log.i(getString(R.string.TAG), getString(R.string.recoverButtonData));
        int index = 1;
        for(ImageButton element: buttonList) {
            if(thirtyGameManager.isDiceSelected(index)){
                element.setImageResource(
                        chooseDieColour(ColourConstants.GREY, index));
            }else{
                element.setImageResource(
                        chooseDieColour(ColourConstants.WHITE, index));
            }
            index++;
        }
    }

    @Override
    public void onRestoreInstanceState(Bundle savedInstanceState) {
        Log.d(getString(R.string.TAG), getString(R.string.onRestoreInstanceState));
        super.onRestoreInstanceState(savedInstanceState);

        thirtyGameManager  = savedInstanceState.getParcelable(getString(R.string.KEY_THIRTYGAMEMANAGER));
        runningResultText.setText(savedInstanceState.getString(getString(R.string.KEY_RUNNINGRESULTTEXT)));
    }



    /**
     * Initialise the class
     */
    private void Initialise(){
        Log.i(getString(R.string.TAG), getString(R.string.Initialise));
        setDicedRolled(NO);
        //add the id of each image button
        updateListOfDieId();
        //add a listener to the roll dice button
        initialiseRollDiceButton();
        //all die should be white with random value
        initialiseDieButtonValue();
        //add a listener to the calculate dice button
        initialiseCalculateDiceButton();

        printStartUpMessage();
    }

    /**
     * Add listeners to roll die buttons. When a die button is clicked it is either toggled
     * between selected and unselected. When a die button is selected its colour is grey otherwise
     * the colour is white.
     * @param dieButtonId the die button to add a listener to.
     * @param index choose die object
     * */
    private void dieButtonController(final ImageButton dieButtonId, final int index){
        dieButtonId.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                toggleSelectUnselectDie(dieButtonId, index);
            }
        });
    }

    /**
     * Toggle die from selected to unselected and vice versa.
     * */
    private void toggleSelectUnselectDie(ImageButton dieButtonId, int index){
        if(isGameRunning() && hasRolledDice()){
            if (thirtyGameManager.isDiceSelected(index)) {
                thirtyGameManager.unSelect(index);
                dieButtonId.setImageResource(chooseDieColour(ColourConstants.WHITE, index));
            } else {
                thirtyGameManager.select(index);
                dieButtonId.setImageResource(chooseDieColour(ColourConstants.GREY, index));
            }
        }else{
            makeShortToast(R.string.cannot_select_dice);
            gameOverToast();
        }
    }

    private void setDicedRolled(boolean status){
        dicedRolled = status;
    }

    private boolean getDicedRolled(){
        return dicedRolled;
    }

    private boolean hasRolledDice(){
        return getDicedRolled();
    }
    /**
     * Reset the die buttons.
     */
    private void resetDieButtons(){
        initialiseDieButtonValue();
        setDicedRolled(NO);
    }

    /**
     * Add a on click listener to the calculate dice button
     */
    private  void initialiseCalculateDiceButton(){
        calculateDiceButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(isGameRunning()) {//has a game commenced
                    updateGameResults();//Update the game results
                }else {
                    mToastGameNotStarted(LONG);
                    gameOverToast();
                }
                showGameResults();//show the final results of the game
            }
        });
    }

    /**
     * Update the game results
     */
    private void updateGameResults(){
        if (thirtyGameManager.isDiceSelected()) {//have Die been selected
            tallyScore();//Tally the running score
            resetDieButtons();
            printCurrentResults();
        } else {
            makeLongToast(R.string.calculate_die);
        }
    }

    /**
     * Tally the running score
     */
    private void tallyScore(){
        thirtyGameManager.tallyDiceScore();//calculate dice score
    }

    /**
     * Toast: game has not begun.
     */
    private void mToastGameNotStarted(boolean duration){
        if(duration){
            makeLongToast(R.string.commence_game);
        }else {
            makeShortToast(R.string.commence_game);
        }
    }


    /**
     * Add a on click listener to the roll dice button. When clicked roll all free die.
     * */
    private void initialiseRollDiceButton(){
        rollDiceButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(canPlayerRollDice() && isGameRunning()){
                    playerThrowDice();//player throw dice
                    setDicedRolled(YES);
                }else{
                    noMoreDiceThrows();//inform UI if user has no throws remaining
                    gameOverToast();
                }
            }
        });
    }

    private boolean isGameRunning(){
        return thirtyGameManager.getGameStatus();
    }


    private boolean canPlayerRollDice(){
        return thirtyGameManager.canRollDiceForPlayer();
    }

    /**
     * A player throws his unselected dice.
     * */
    private void playerThrowDice(){
        int index = 1;
        //choose a button
        for (ImageButton element : buttonList){
            if(!thirtyGameManager.isDiceSelected(index)) {
                //roll unselected dice
                thirtyGameManager.rollDice(index);
                //unselected dice colour is white
                element.setImageResource(chooseDieColour(ColourConstants.WHITE, index));
            }
            index++;//next die value
        }
        //Player throw the dice this round
        thirtyGameManager.playerRolledDice();
    }

    /**
     * Display a toast informing that the game has ended
     */
    private void gameOverToast(){
        if(!isGameRunning()){
            makeLongToast(R.string.game_over);
        }
    }
    /**
     * Inform UI if user has no throws remaining
     * */
    private void noMoreDiceThrows(){
        if (!thirtyGameManager.canRollDiceForPlayer()){
            makeShortToast(R.string.last_click);
        }
    }

    public void printCurrentResults(){
        Log.i(getString(R.string.TAG), getString(R.string.printCurrentResults));
        /*String msg = "\nRound: " + thirtyGameManager.getGameRounds() + "\nScore: "+
                thirtyGameManager.getScoreForRound()+ "\nTotal: " + thirtyGameManager.getTotalScoreForGame();*/

        if(isGameRunning()){
            textViewMessage(getString(R.string.Round) +
                    thirtyGameManager.getGameRounds() +
                    getString(R.string.Score) +
                    thirtyGameManager.getScoreForRound() +
                    getString(R.string.Total) +
                    thirtyGameManager.getTotalScoreForGame());
        }else{
            textViewMessage(getString(R.string.ThatsItGameOver) +
                    getString(R.string.Round) +
                    thirtyGameManager.getGameRounds() +
                    getString(R.string.Score) +
                    thirtyGameManager.getScoreForRound() +
                    getString(R.string.Total) +
                    thirtyGameManager.getTotalScoreForGame());
        }
    }

    private void printStartUpMessage(){
        Log.i(getString(R.string.TAG), getString(R.string.printStartUpMessage));

        textViewMessage(getString(R.string.gameInstructions));
    }

    private void textViewMessage(String msg){
        runningResultText.setText(msg);

    }
    /**
     * Short popup message.
     * */
    private void makeShortToast(int msg){
        Toast.makeText(getApplicationContext(), msg, Toast.LENGTH_SHORT).show();
    }

    /**
     * Long popup message.
     * */
    private void makeLongToast(int msg){
        Toast.makeText(getApplicationContext(), msg, Toast.LENGTH_LONG).show();
    }

    /**
     * Initialise all the buttons to a random white die value.
     * */
    private void initialiseDieButtonValue(){
        int index = 1;
        //choose a button
        for (ImageButton element : buttonList){
            element.setImageResource(
                    chooseDieColour(ColourConstants.WHITE, index)
            );
            dieButtonController(element, index);
            index++;//next die value
        }
    }

    /**
     * Choose the colour of a die and its value.
     * @param colour white, red or grey as int
     * @param value what is the value of the die as int
     * @return a die with a colour and value as int
     * */
    private int chooseDieColour(ColourConstants colour, int value){
        int res;
        switch (colour){
            case WHITE:
                res = whiteButton(thirtyGameManager.getDieValue(value));
                break;
            case RED:
                res = redButton(thirtyGameManager.getDieValue(value));
                break;
            case GREY:
                res = greyButton(thirtyGameManager.getDieValue(value));
                break;
            default:
                res = whiteButton(thirtyGameManager.getDieValue(value));
        }
        return res;
    }

    /**
     * Find a add the id of each image button to a collection of image buttons as die.
     * */
    @SuppressLint("FindViewByIdCast")
    private void updateListOfDieId(){
        buttonList.add(findViewById(R.id.Die_1_button));
        buttonList.add(findViewById(R.id.Die_2_button));
        buttonList.add(findViewById(R.id.Die_3_button));
        buttonList.add(findViewById(R.id.Die_4_button));
        buttonList.add(findViewById(R.id.Die_5_button));
        buttonList.add(findViewById(R.id.Die_6_button));
    }

    /**
     * Get a white die for a value.
     * @param index key value as int
     * @return the id of a drawable white die
     * */
    private int whiteButton(int index){
        int res;
        switch (index){
            case 1:
                res = R.drawable.white1;
                break;
            case 2:
                res = R.drawable.white2;
                break;
            case 3:
                res = R.drawable.white3;
                break;
            case 4:
                res = R.drawable.white4;
                break;
            case 5:
                res = R.drawable.white5;
                break;
            case 6:
                res = R.drawable.white6;
                break;
            default:
                res = R.drawable.white1;
                break;
        }
        return res;
    }

    /**
     * Get a red die for a value.
     * @param index key value as int
     * @return the id of a drawable white die
     * */
    private int redButton(int index){
        int res;
        switch (index){
            case 1:
                res = R.drawable.red1;
                break;
            case 2:
                res = R.drawable.red2;
                break;
            case 3:
                res = R.drawable.red3;
                break;
            case 4:
                res = R.drawable.red4;
                break;
            case 5:
                res = R.drawable.red5;
                break;
            case 6:
                res = R.drawable.red6;
                break;
            default:
                res = R.drawable.red1;
                break;
        }
        return res;
    }

    /**
     * Get a grey die for a value.
     * @param index key value as int
     * @return the id of a drawable white die
     * */
    private int greyButton(int index){
        int res;
        switch (index){
            case 1:
                res = R.drawable.grey1;
                break;
            case 2:
                res = R.drawable.grey2;
                break;
            case 3:
                res = R.drawable.grey3;
                break;
            case 4:
                res = R.drawable.grey4;
                break;
            case 5:
                res = R.drawable.grey5;
                break;
            case 6:
                res = R.drawable.grey6;
                break;
            default:
                res = R.drawable.grey1;
                break;
        }
        return res;
    }
}
