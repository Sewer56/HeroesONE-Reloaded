using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reloaded_GUI.Styles.Controls.Animated;

namespace HeroesONE_R_GUI.Controls
{
    /// <summary>
    /// Overrides the animated data grid view with a custom drop implementation for drag - drop.
    /// </summary>
    public class CustomDataGridView : AnimatedDataGridView
    {
        /// <summary>
        /// Provides a customised drag event for HeroesONE Reloaded.
        /// </summary>
        [Browsable(true)]
        public event EventHandler<DragEventArgs> CustomDragDropEvent;

        /// <summary>
        /// Call the custom drag event if it is set.
        /// </summary>
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            CustomDragDropEvent?.Invoke(this, drgevent);
        }
    }
}
