using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace BMWControlApp
{
    public class MainAdapter : RecyclerView.Adapter
    {
        private readonly Dictionary<string, string> carInfo;

        public MainAdapter(Dictionary<string, string> carInfo)
        {
            this.carInfo = carInfo;
        }

        //BIND DATA TO VIEWS
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolder h = holder as ViewHolder;
            h.NameTxt.Text = carInfo.ElementAt(position).Key;
            h.InfoText.Text = carInfo.ElementAt(position).Value;

        }

        //INITIALIZE VH
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //INFLATE LAYOUT TO VIEW
            View v = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Model, parent, false);
            ViewHolder holder = new ViewHolder(v);

            return holder;
        }

        //TOTAL NUM OF GALAXIES
        public override int ItemCount
        {
            get { return carInfo.Count(); }
        }

        /*
         * Our Viewholder class.
         * Will hold our textview.
         */
        internal class ViewHolder : RecyclerView.ViewHolder
        {
            public TextView NameTxt;
            public TextView InfoText;

            public ViewHolder(View itemView)
                : base(itemView)
            {
                NameTxt = itemView.FindViewById<TextView>(Resource.Id.nameTxt);
                InfoText = itemView.FindViewById<TextView>(Resource.Id.infoTxt);
            }
        }
    }
}