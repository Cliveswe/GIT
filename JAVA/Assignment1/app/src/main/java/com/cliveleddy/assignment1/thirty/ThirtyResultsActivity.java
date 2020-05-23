package com.cliveleddy.assignment1.thirty;

import android.graphics.Color;
import android.os.Bundle;
import android.os.CountDownTimer;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import Model.ThirtyGameManager;

public class ThirtyResultsActivity extends AppCompatActivity {
    private TextView scoreResults, endGame, playAgain;
    private Bundle resultBundle;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_results_thirty);
        Log.i(getString(R.string.TAG_ThirtyResultActivity), getString(R.string.onCreateInThirtyResultActivity));


        scoreResults = findViewById(R.id.thirty_results_list);//location to print results
        endGame = findViewById(R.id.end_game);
        playAgain= findViewById(R.id.play_again);

        //receive instructions from ThirtyActivity
        resultBundle = getIntent().getExtras();
        //generate results
        if (resultBundle != null) {
            initialize();
            printResults();

        }else{
            Log.v(getString(R.string.TAG), getString(R.string.onCreateSomethingWrong));
        }
    }

    private void printResults(){
        ThirtyGameManager resultIntent = resultBundle.getParcelable(getString(R.string.ThirtyGameManager));
        String msg = resultAsText(resultIntent);
        scoreResults.append(msg);
    }

    /**
     * Make the end game text change colour when it is pressed.
     */
    private void setEndGameAction(){
        final int t = 250;

        endGame.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                setEndGAmeTextView(Color.RED, Color.WHITE);
                setEndGameFlashTimer(t, t); //count down timer
            }
        });
    }

    /**
     * Make the end game text change colour when it is pressed.
     */
    private void setPlayAgainAction(){
        final int t = 250;

        playAgain.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Log.i(getString(R.string.TAG_ThirtyResultActivity),"setPlayAgainAction..!");
                setPlayAgainTextView(Color.RED, Color.WHITE);
                setPlayAgainGameFlashTimer(t, t); //count down timer
                Log.i(getString(R.string.TAG_ThirtyResultActivity),"setPlayAgainAction done..!");

            }
        });
    }
    /**
     * When the countdown timer is activate it performs an action when it has completed its task.
     * It is possible to perform additional tasks during the countdown.
     * @param time the duration of the timer
     * @param countDownInterval an interval with in the duration of the timer
     */
    private void setPlayAgainGameFlashTimer(int time, int countDownInterval ){
        new CountDownTimer(time, countDownInterval) {
            @Override
            public void onTick(long l) {
            }

            @Override
            public void onFinish() {
                setPlayAgainTextView(Color.BLUE, Color.WHITE);
                setResult(-1,null);//play again
                finish();//close the activity
            }
        }.start();
    }

    /**
     * When the countdown timer is activate it performs an action when it has completed its task.
     * It is possible to perform additional tasks during the countdown.
     * @param time the duration of the timer
     * @param countDownInterval an interval with in the duration of the timer
     */
    private void setEndGameFlashTimer(int time, int countDownInterval ){
        new CountDownTimer(time, countDownInterval) {
            @Override
            public void onTick(long l) {
            }

            @Override
            public void onFinish() {
                setEndGAmeTextView(Color.BLUE, Color.WHITE);
                setResult(100, null);//end the game
                finish();//close the activity
            }
        }.start();
    }

    /**
     * Initialize UI
     * */
    private void initialize(){
        setEndGAmeTextView(Color.BLUE, Color.WHITE);
        setPlayAgainTextView(Color.BLUE, Color.WHITE);

        setEndGameAction();
        setPlayAgainAction();
    }

    private void setEndGAmeTextView(int bg, int txt){
        endGame.setBackgroundColor(bg);
        endGame.setTextColor(txt);
    }

    private void setPlayAgainTextView(int bg, int txt){
        playAgain.setBackgroundColor(bg);
        playAgain.setTextColor(txt);
    }

    /**
     * Generate the results of the game as text.
     * @param resultIntent Thirty game manager received via Intent/Bundle
     * @return a formatted message of the players results for this game.
     */
    private String resultAsText(ThirtyGameManager resultIntent){
        String msg;
        int maxScore = -1;
        int maxScoreRound = 0;
        int index = 1;//for humans to read

        for(Integer element: resultIntent.getAllScoresForPlayer()){
            msg = String.format(getString(R.string.GameResultsForRound), index, element);
            scoreResults.append(msg);
            //record the maximum score and in what round it occurred
            if(maxScore < element){
                maxScore = element;
                maxScoreRound = index;
            }
            index++;
        }
        //total score
        msg = String.format(getString(R.string.GameResultsForTotalScore), resultIntent.getTotalScoreForGame());
        scoreResults.append(msg);
        //best result in round
        msg = String.format(getString(R.string.GameResultsBestScore), maxScoreRound, maxScore);
        return msg;
    }

    @Override
    protected void onSaveInstanceState(Bundle savedInstanceState) {
        super.onSaveInstanceState(savedInstanceState);
        Log.i(getString(R.string.TAG_ThirtyResultActivity), getString(R.string.onSaveInstanceStateThirtyResultActivity));
    }
}