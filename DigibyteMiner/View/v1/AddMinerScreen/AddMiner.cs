using OneMiner.Core;
using OneMiner.Core.Interfaces;
using OneMiner.View.v1.AddMinerScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneMiner.View.v1
{
    public partial class AddMiner : Form
    {
        private int m_currentAlgoIndex = 0;
        private int m_currentCoinIndex = 0;
        private IMinerContainer m_parent = null;
        public  IHashAlgorithm DefaultAlgorithm {get;set;}
        public ICoin DefaultCoin { get; set; }
        public string Minername { get; set; }

        public AddMiner(IMinerContainer parent)
        {
            m_parent = parent;
            InitializeComponent();
        }
        private bool AlgorithmSelected()
        {
            try
            {
                if ((lbAlgoSelect.SelectedIndex >= 0 && lbAlgoSelect.SelectedIndex <= (lbAlgoSelect.Items.Count - 1))
              && (lbCoinSelect.SelectedIndices[0] >= 0 && lbCoinSelect.SelectedIndices[0] <= (lbCoinSelect.Items.Count - 1)))
                {
                    return true;
                }
            }
            catch (Exception es)
            {
            }
            return false;
        }
        //Todo: check in core if no miners with this name exists
        private bool UniqueMinerName(string name)
        {
            return true;
        }
        private bool NameAdded()
        {
            Minername = txtMinername.Text.Trim();
            if (Minername.Length > 0 && UniqueMinerName(Minername))
            {
                return true;
            }
            return false;
        }
        public void SetNextButtonState()
        {
            if (AlgorithmSelected() && NameAdded())
                m_parent.EnableNextButton();
            else
                m_parent.DisableNextButton();
        }
        public void MakeSelectedCoin()
        {
            if (DefaultCoin != null)
            {
                m_parent.MakeSelectedCoin(DefaultCoin);
                lblSelectedCoin.Text = DefaultCoin.Name;
                pbSelectedMiner.Image = DefaultCoin.Logo;
            }
            else
                lblSelectedCoin.Text = "No Coin Selected";

        }
        private void SelectFirstAlgo(int index,IHashAlgorithm algo)
        {
            lbAlgoSelect.SelectedIndex = index;
            DisplayCoinsinList(algo);

        }
        private void DisplayCoinsinList(IHashAlgorithm algo)
        {
            DefaultCoin = algo.DefaultCoin;
            ImageList Imagelist = new ImageList();
            Imagelist.ImageSize = new Size(25, 25);
            foreach (ICoin item in algo.SupportedCoins)
            {
                Imagelist.Images.Add(item.Logo);
            }
            lbCoinSelect.LargeImageList = Imagelist;
            lbCoinSelect.SmallImageList = Imagelist;
            int i = 0;
            foreach (ICoin item in algo.SupportedCoins)
            {
                lbCoinSelect.Items.Add(new ListViewItem { ImageIndex = i, Text = item.Name });


                //lbCoinSelect.Items.Add(item.Name);
                if (item == DefaultCoin)
                    m_currentCoinIndex = i;
                i++;
            }

            lbCoinSelect.Items[m_currentCoinIndex].Selected = true;
            //lbCoinSelect.SelectedIndex = m_currentCoinIndex;


        }

        private void AddMiner_Load(object sender, EventArgs e)
        {
            try
            {
                txtMinername.Text = Minername;
                if (DefaultAlgorithm == null)//when editing this wil be preset and not null
                    DefaultAlgorithm = Factory.Instance.DefaultAlgorithm;

                List<IHashAlgorithm> algos = Factory.Instance.Algorithms;

                int i=0;
                foreach (IHashAlgorithm item in algos)
                {
                    lbAlgoSelect.Items.Add(item.Name);
                    if (item.Name == DefaultAlgorithm.Name)
                        m_currentAlgoIndex = i;
                    i++;

                }
                //by default select the first algo
                SelectFirstAlgo(m_currentAlgoIndex, DefaultAlgorithm);
                lbAlgoSelect.SelectedIndexChanged += lstAlgoSelect_SelectedIndexChanged;
                lbCoinSelect.SelectedIndexChanged += lbCoinSelect_SelectedIndexChanged;
                //check and eneble next button
                SetNextButtonState();
                //Tell parent which coin is currently seelcted
                MakeSelectedCoin();
            }
            catch (Exception ex)
            {
            }
            
        }

        void lbCoinSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = lbCoinSelect.SelectedIndices[0];

                if (m_currentCoinIndex == index)
                    return;
                List<ICoin> coins = DefaultAlgorithm.SupportedCoins;

                DefaultCoin = coins[index];
                m_currentCoinIndex = index;

                SetNextButtonState();
                //Tell parent which coin is currently seelcted
                MakeSelectedCoin();

            }
            catch (Exception)
            {

            }
        }

        void txtMinername_TextChanged(object sender, EventArgs e)
        {
            Minername = txtMinername.Text.Trim();
            SetNextButtonState();

        }

        void lstAlgoSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = lbAlgoSelect.SelectedIndex;

                if (m_currentAlgoIndex == index)
                    return;
                lbCoinSelect.Items.Clear();
                List<IHashAlgorithm> algos = Factory.Instance.Algorithms;

                DefaultAlgorithm = algos[index];
                m_currentAlgoIndex = index;

                DisplayCoinsinList(DefaultAlgorithm);
                SetNextButtonState();
                //Tell parent which coin is currently seelcted
                MakeSelectedCoin();
               
            }
            catch (Exception )
            {

            }
         
        }





    }
}
