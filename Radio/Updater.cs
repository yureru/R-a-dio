﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using System.Windows.Controls;

namespace Radio
{
    /// <summary>
    /// Serves to update the TextBox in the application, and also calls the update counter
    /// of the Song object (TickerAndUpdate())
    /// </summary>
    static class Updater
    {
        static bool _hasStarted = false;
        /// TODO: Update description.
        /// <summary>
        /// Calls the counter update, and checks the _hasStarted variable to run the if for the first time.
        /// </summary>
        /// <param name="Current"></param>
        /// <param name="Song"></param>
        /// <param name="DJ"></param>
        /// <param name="Listeners"></param>
        /// <param name="StartTime"></param>
        /// <param name="CurrentTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="Sldr"></param>
<<<<<<< HEAD
<<<<<<< HEAD
        public static async void NeedToUpdate(Song Current, TextBlock Song, TextBlock DJ, TextBlock Listeners, TextBlock CurrentTime, TextBlock EndTime, Slider Sldr, Image Img, Timer timer, HandleException err)
        {   
            if (Current.ShouldUpdateSong() /*|| !_hasStarted*/)
            {

                await Current.GetNewSongData(err);
                
=======
=======
>>>>>>> parent of 32e932c... Better responsiveness
        public static void NeedToUpdate(ref Song Current, ref TextBlock Song, ref TextBlock DJ, ref TextBlock Listeners, ref TextBlock CurrentTime, ref TextBlock EndTime, ref Slider Sldr, ref Image Img)
        {
            if (Current.TickerAndUpdate() || !_hasStarted)
            {
<<<<<<< HEAD
>>>>>>> parent of 32e932c... Better responsiveness
=======
>>>>>>> parent of 32e932c... Better responsiveness
                Song.Text = Current.Name;
                DJ.Text = Current.Dj;
                Listeners.Text = Current.Listeners.ToString();
                CurrentTime.Text = Current.CurrentTime;
                EndTime.Text = Current.EndTime;
                Sldr.Maximum = Current.DoubleEndTime;
                Sldr.Value = 0;
                Current.Image.IsNewDjPlaying(Current.DjId);
                Current.Image.LoadNewImage();
                Img.Source = Current.Image.Image;
                _hasStarted = true;
            }
            else
            {
                CurrentTime.Text = Current.CurrentTime;
                Sldr.Value = Current.DoubleCurrentTime;
            }
        }
    }
}
