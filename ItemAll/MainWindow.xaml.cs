using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using ItemAll.FileManager;
using System.ComponentModel;
using ItemAll.Forms.Message;
using FieryLib.Models;

namespace ItemAll
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //GLOBAL_MENU.IsEnabled = false;
            p_progress.Visibility = Visibility.Hidden;
            p_label.Visibility = Visibility.Hidden;
        }

        private BackgroundWorker bw_LoadFile;

        private void b_open_Click(object sender_, RoutedEventArgs e_)
        {
            p_progress.Visibility = Visibility.Visible;
            p_label.Visibility = Visibility.Visible;

            bw_LoadFile = new BackgroundWorker();
            bw_LoadFile.DoWork += (sender, e) => 
            {
                LCIO.OpenFile(bw_LoadFile);
            };
            bw_LoadFile.ProgressChanged += (sender, e) =>
            {
                p_label.Content = e.UserState;
            };
            bw_LoadFile.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                {
                    new TextMessage("Ошибка", e.Error.Message).ShowDialog();
                }
                p_progress.Visibility = Visibility.Hidden;
                p_label.Visibility = Visibility.Hidden;
                FLYOUT_OpenFile.IsOpen = false;
                UpdateList();
            };
            bw_LoadFile.WorkerReportsProgress = true;
            bw_LoadFile.RunWorkerAsync();
        }

        private void UpdateList()
        {
            foreach(var item in LCIO.ITEM_ALL)
            {
                L_Box.Items.Add(item.ItemID + " - "+ item.Name);
            }
        }

        private ItemAllLod GetItem()
        {
            if (L_Box.SelectedIndex == -1) return null;

            int idx = -1;
            int.TryParse(L_Box.SelectedItem.ToString().Split(new char[] { ' ' })[0], out idx);

            if (idx == -1)
                return null;

            return LCIO.ITEM_ALL.Find(p => p.ItemID == idx);
        }

        private void CheckTypeOption(int idx)
        {
            if (idx > 0)
            {
                ItemAllLod rare = LCIO.ITEM_ALL.Find(p => p.ItemID == idx);
                if (rare != null)
                {
                    if (rare.Type == 4 && (rare.SubType == 16 || rare.SubType == 22))
                        C_Option_Type.SelectedIndex = 0;
                    else
                        C_Option_Type.SelectedIndex = 1;
                }
                else
                    C_Option_Type.SelectedIndex = 1;
            }
            else if (idx == -1)
                C_Option_Type.SelectedIndex = 0;
        }

        private void L_Box_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (L_Box.SelectedIndex == -1) return;

            ItemAllLod item = GetItem();

            if (item == null) return;

            T_ID.Text = item.ItemID.ToString();
            T_Name.Text = item.Name;
            T_Desc.Text = item.Desc;
            T_SMC.Text = item.SMC;
            T_Type.Text = item.Type.ToString();
            T_SubType.Text = item.SubType.ToString();
            T_Position.Text = item.Wearing.ToString();
            T_TypeChar.Text = item.JobFlag.ToString();
            T_MaxUse.Text = item.MaxUse.ToString();
            T_ICO_ID.Text = item.TexID.ToString();
            T_ICO_LINE.Text = item.TexRow.ToString();
            T_ICO_COLUMN.Text = item.TexCol.ToString();
            T_Flag.Text = item.Flag.ToString();
            T_Price.Text = item.Price.ToString();
            T_MaxCount.Text = item.Weight.ToString();
            T_Level.Text = item.Level.ToString();
            T_P_ID.Text = item.RareOption.ToString();
            T_P_Rait.Text = item.RareChances.ToString();
            T_Num_0.Text = item.num0.ToString();
            T_Num_1.Text = item.num1.ToString();
            T_Num_2.Text = item.num2.ToString();
            T_Num_3.Text = item.num3.ToString();
            T_Rand1.Text = item.set1.ToString();
            T_Rand2.Text = item.set2.ToString();
            T_Rand3.Text = item.set3.ToString();
            T_Rand4.Text = item.set4.ToString();
            T_Rand5.Text = item.set5.ToString();
            T_NeedItem_0.Text = item.NeedItem[0].ToString();
            T_NeedItem_1.Text = item.NeedItem[1].ToString();
            T_NeedItem_2.Text = item.NeedItem[2].ToString();
            T_NeedItem_3.Text = item.NeedItem[3].ToString();
            T_NeedItem_4.Text = item.NeedItem[4].ToString();
            T_NeedItem_5.Text = item.NeedItem[5].ToString();
            T_NeedItem_6.Text = item.NeedItem[6].ToString();
            T_NeedItem_7.Text = item.NeedItem[7].ToString();
            T_NeedItem_8.Text = item.NeedItem[8].ToString();
            T_NeedItem_9.Text = item.NeedItem[9].ToString();
            T_NeedProb_0.Text = item.NeedItemCount[0].ToString();
            T_NeedProb_1.Text = item.NeedItemCount[1].ToString();
            T_NeedProb_2.Text = item.NeedItemCount[2].ToString();
            T_NeedProb_3.Text = item.NeedItemCount[3].ToString();
            T_NeedProb_4.Text = item.NeedItemCount[4].ToString();
            T_NeedProb_5.Text = item.NeedItemCount[5].ToString();
            T_NeedProb_6.Text = item.NeedItemCount[6].ToString();
            T_NeedProb_7.Text = item.NeedItemCount[7].ToString();
            T_NeedProb_8.Text = item.NeedItemCount[8].ToString();
            T_NeedProb_9.Text = item.NeedItemCount[9].ToString();
            T_Skill_ID_1.Text = item.SSkillId1.ToString();
            T_Skill_ID_2.Text = item.SSkillId2.ToString();
            T_Skill_Level_1.Text = item.SSkillLevel1.ToString();
            T_Skill_Level_2.Text = item.SSkillLevel2.ToString();
            T_Effect_Normal.Text = item.Effect1.ToString();
            T_Effect_Attack.Text = item.Effect2.ToString();
            T_Effect_Damage.Text = item.Effect3.ToString();
            T_Fortune.Text = item.FortuneIndex.ToString();
            T_Fortune.Text = item.FortuneIndex.ToString();
            T_RVR_Value.Text = item.RvRValue.ToString();

            CheckTypeOption(item.RareIndexes[0]);

            T_Option_0.Text = item.RareIndexes[0].ToString();
            T_Option_1.Text = item.RareIndexes[1].ToString();
            T_Option_2.Text = item.RareIndexes[2].ToString();
            T_Option_3.Text = item.RareIndexes[3].ToString();
            T_Option_4.Text = item.RareIndexes[4].ToString();
            T_Option_5.Text = item.RareIndexes[5].ToString();
            T_Option_6.Text = item.RareIndexes[6].ToString();
            T_Option_7.Text = item.RareIndexes[7].ToString();
            T_Option_8.Text = item.RareIndexes[8].ToString();
            T_Option_9.Text = item.RareIndexes[9].ToString();

            T_Option_0_Prob.Text = item.RareProbs[0].ToString();
            T_Option_1_Prob.Text = item.RareProbs[1].ToString();
            T_Option_2_Prob.Text = item.RareProbs[2].ToString();
            T_Option_3_Prob.Text = item.RareProbs[3].ToString();
            T_Option_4_Prob.Text = item.RareProbs[4].ToString();
            T_Option_5_Prob.Text = item.RareProbs[5].ToString();
            T_Option_6_Prob.Text = item.RareProbs[6].ToString();
            T_Option_7_Prob.Text = item.RareProbs[7].ToString();
            T_Option_8_Prob.Text = item.RareProbs[8].ToString();
            T_Option_9_Prob.Text = item.RareProbs[9].ToString();  

            C_CastleWar.SelectedIndex = item.CastleWar;
            C_RVR_Type.SelectedIndex = item.RvRValue;

            L_Option.Items.Clear();
            L_Option_Prob.Items.Clear();
        }

        private void ChangetTextOption(object sender, TextChangedEventArgs e)
        {
            TextBox com = (TextBox)sender;
            char num =  com.Name[com.Name.Length - 1];
            TextBox com_d = (TextBox)FindName("T_Option_" + num + "_D");

            int i = -2;
            int.TryParse(com.Text, out i);
            if (i == -2) return;
            if (i < 0 || (i == 0 && C_Option_Type.SelectedIndex == 1))
            {
                com_d.Text = "Не выбрано";

                if(L_Option.Items.Count > 0)
                {
                    L_Option.SelectedIndex = 0;
                }
                return;
            }

            if (C_Option_Type.SelectedIndex == 0)
            {
                int idx = LCIO.OPTION.FindIndex(p => p.Type == i);
                if(idx == -1)
                {
                    com.Text = "-1";
                    return;
                }
                com_d.Text = LCIO.OPTION_NAME[idx].m_index + " - " + LCIO.OPTION_NAME[idx].m_name;
                if(L_Option.Items.Count > 0)
                {
                    if(LastFocusOption == 'N')
                        L_Option.SelectedIndex = LCIO.OPTION.FindIndex(p => p.Type == LCIO.OPTION_NAME[idx].m_index) + 1;
                    L_Option.ScrollIntoView(L_Option.SelectedItem);
                }

                if (com.IsFocused)
                {
                    L_Option_Prob.Items.Clear();
                    L_Option_Prob.Items.Add("Не выбрано");
                    foreach (var prob in LCIO.OPTION[idx].Levels)
                    {
                        L_Option_Prob.Items.Add(prob);
                    }

                    TextBox com2 = (TextBox)FindName("T_Option_" + num + "_Prob");
                    com2.Text = "0";
                }
            }
            else if(C_Option_Type.SelectedIndex == 1)
            {
                int idx = LCIO.RARE.FindIndex(p => p.RareOptionID == i);
                com_d.Text = LCIO.RARE_NAME[idx].m_index + " - " + LCIO.RARE_NAME[idx].m_name;
                if (L_Option.Items.Count > 0)
                {
                    if (LastFocusOption == 'N')
                        L_Option.SelectedIndex = LCIO.RARE.FindIndex(p => p.RareOptionID == LCIO.RARE_NAME[idx].m_index) + 1;
                    L_Option.ScrollIntoView(L_Option.SelectedItem);
                }
            }
        }

        private void FocusTextOption(object sender, RoutedEventArgs e)
        {
            TextBox com = (TextBox)sender;
            char num = com.Name.Last();

            int i = -2;
            int.TryParse(com.Text, out i);
            if (i < 0) return;

            if (C_Option_Type.SelectedIndex == 0)
            {
                int idx = LCIO.OPTION.FindIndex(p => p.Type == i);
                L_Option.Items.Clear();
                L_Option.Items.Add("Не выбрано");
                foreach(var opt in LCIO.OPTION_NAME)
                {
                    L_Option.Items.Add(opt.m_index + " - " + opt.m_name);
                }

                LastFocusOption = num;
                L_Option.SelectedIndex = idx + 1;
                
                L_Option.ScrollIntoView(L_Option.SelectedItem);

                L_Option_Prob.Items.Clear();
                L_Option_Prob.Items.Add("Не выбрано");
                foreach (var prob in LCIO.OPTION[idx].Levels)
                {
                    L_Option_Prob.Items.Add(prob);
                }

                if (L_Option_Prob.Items.Count > 0)
                {
                    TextBox com2 = (TextBox)FindName("T_Option_" + num + "_Prob");

                    L_Option_Prob.SelectedIndex = Convert.ToInt32(com2.Text);
                    L_Option_Prob.ScrollIntoView(L_Option_Prob.SelectedItem);
                }
            }
            else if(C_Option_Type.SelectedIndex == 1)
            {
                int idx = LCIO.RARE.FindIndex(p => p.RareOptionID == i);
                L_Option.Items.Clear();
                L_Option.Items.Add("Не выбрано");
                foreach (var opt in LCIO.RARE_NAME)
                {
                    L_Option.Items.Add(opt.m_index + " - " + opt.m_name);
                }

                LastFocusOption = num;
                L_Option.SelectedIndex = idx + 1;

                L_Option.ScrollIntoView(L_Option.SelectedItem);

            }
        }

        private void ChangetTextOptionProb(object sender, TextChangedEventArgs e)
        {
            TextBox com = (TextBox)sender;
            string num = com.Name.Split(new char[] { '_'})[2];
            TextBox com2 = (TextBox)FindName("T_Option_" + num);

            if (C_Option_Type.SelectedIndex == 0)
            {
                OptionLod lod = LCIO.OPTION.Find(p => p.Type == Convert.ToInt32(com2.Text));

                int selectNum = -1;
                int.TryParse(com.Text, out selectNum);

                TextBox com3 = (TextBox)FindName("T_Option_" + num + "_Prob_D");
                if (selectNum < 1)
                {
                    com3.Text = "Не выбрано";
                    return;
                }
                
                if (selectNum >= lod.Levels.Length)
                {
                    selectNum = 1;
                    com.Text = "1";
                }
                com3.Text = lod.Levels[selectNum-1].ToString();

                if (L_Option_Prob.Items.Count > 0)
                {
                    L_Option_Prob.SelectedIndex = selectNum;
                    L_Option_Prob.ScrollIntoView(L_Option_Prob.SelectedItem);
                }
            }
        }
        static bool isFocusProb = false;
        private void FocusTextOptionProb(object sender, RoutedEventArgs e)
        {
            if (!isFocusProb)
            {
                TextBox com = (TextBox)sender;
                string num = com.Name.Split(new char[] { '_' })[2];

                TextBox com2 = (TextBox)FindName("T_Option_" + num);
                com2.Focus();
                isFocusProb = true;
                com.Focus();
            }
            else
            {
                isFocusProb = false;
            }
        }
        static char LastFocusOption = 'N';
        private void L_Option_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (L_Option.SelectedIndex == -1) return;

            if(LastFocusOption != 'N')
            {
                if (L_Option.SelectedIndex == 0)
                    return;

                TextBox com = (TextBox)FindName("T_Option_" + LastFocusOption);

                int idxSelect = Convert.ToInt32(L_Option.SelectedItem.ToString().Split(new char[] { ' ' })[0]);                

                if(C_Option_Type.SelectedIndex == 0)
                {
                    OptionLod lod = LCIO.OPTION.Find(p => p.OptionID == idxSelect);
                    com.Text = lod.Type.ToString();
                    com.Focus();
                }
                else if(C_Option_Type.SelectedIndex == 1)
                {
                    RareOptionLod lod = LCIO.RARE.Find(p => p.RareOptionID == idxSelect);
                    com.Text = lod.RareOptionID.ToString();
                    com.Focus();
                }
            }
        }

        private void TypeCharText(object sender, TextChangedEventArgs e)
        {
            int res = -1;
        }
    }
}
