package com.cliveleddy.mycookbook;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v7.app.AppCompatActivity;

/**
 * A generic instantiation of a fragment.
 */
public abstract class SingleFragmentActivity extends AppCompatActivity {
    protected abstract Fragment createFragment();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_fragment);//activity's view to be inflated

        FragmentManager fm = getSupportFragmentManager();//get the fragment manager
        Fragment fragment = fm.findFragmentById(R.id.fragment_container);//retrieve the fragment

        if(fragment == null){//if the fragment does not exist, create it

            fragment = createFragment();//instantiate the fragment
            //create, include one add and commit the fragment transaction
            fm.beginTransaction().add(R.id.fragment_container, fragment).commit();
        }
    }
}
