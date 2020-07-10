package com.cliveleddy.mycookbook;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Point;

class PictureUtils {

    /**
     * Scale an image.
     * @param path
     * @param destWidth
     * @param destHeight
     * @return A Bitmapped scaled down image.
     */
    private static Bitmap getScaledBitmap(String path, int destWidth, int destHeight){
        //read in the dimensions of the image on storage device
        BitmapFactory.Options options = new BitmapFactory.Options();
        options.inJustDecodeBounds = true;
        BitmapFactory.decodeFile(path, options);

        float srcWidth = options.outWidth;
        float srcHeight = options.outHeight;

        //determine by how much to scale down
        int inSampleSize = 1;
        if((srcHeight > destHeight) || (srcWidth > destWidth)){
            float heightScale = srcHeight / destHeight;
            float widthScale = srcWidth / destWidth;
            //determine the size of each sample for each pixel
            inSampleSize = Math.round(heightScale > widthScale ? heightScale: widthScale);
        }

        options =  new BitmapFactory.Options();
        options.inSampleSize = inSampleSize;

        //load and create final bitmap
        return BitmapFactory.decodeFile(path, options);


    }

    /**
     * Detriment the size of the screen and then scale the image down to that size.
     * @param path String
     * @param activity Activity
     * @return A Bitmapped scaled down image with a best guess size.
     */
    public static Bitmap getScaledBitmap(String path, Activity activity){
        Point size = new Point();
        activity.getWindowManager().getDefaultDisplay().getSize(size);
        return getScaledBitmap(path, size.x, size.y);
    }
}
