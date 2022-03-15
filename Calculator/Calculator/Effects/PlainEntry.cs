using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace Calculator.Effects
{
    public class PlainEntry : RoutingEffect
    {
        public PlainEntry() : base("PlainEntryGroup.PlainEntryEffect")
        {
        }
    }
}

/*
    <Entry>
        <Entry.Effects>
            <effects:PlainEntry />
        </Entry.Effects>
    </Entry>
 */
