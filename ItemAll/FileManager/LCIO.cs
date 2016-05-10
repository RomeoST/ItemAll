using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using FieryLib.IO;
using FieryLib.Models;
using ItemAll.ExceptionHandler;
using System.ComponentModel;

namespace ItemAll.FileManager
{
    class LCIO
    {
        private static string FILE_OPENED_ITEM_ALL { get; set; } = "";
        private static string FILE_OPENED_ITEM_NAME { get; set; } = "";

        public static List<ItemAllLod> ITEM_ALL { get; set; }
        public static List<OptionLod> OPTION { get; set; }
        public static List<RareOptionLod> RARE { get; set; }
        public static List<StrModel> ITEM_NAME { get; set; }
        public static List<StrModel> OPTION_NAME { get; set; }
        public static List<StrModel> RARE_NAME { get; set; }
        public static void OpenFile(BackgroundWorker bw_LoadFile)
        {
            // Получить позицию исполняемой программы
            string _curDir = Environment.CurrentDirectory;

            FILE_OPENED_ITEM_ALL = _curDir + "/../../Data/itemAll.lod";
            FILE_OPENED_ITEM_NAME = _curDir + "/../../Local/ru/String/strItem_ru.lod";

            // Проверки на существование файлов
            if (!File.Exists(FILE_OPENED_ITEM_ALL)) throw new FileNotFoundExceptionST("Отсутствует itemAll.lod", new Exception());
            if (!File.Exists(_curDir + "/../../Data/option.lod")) throw new FileNotFoundExceptionST("Отсутствует option.lod", new Exception());
            if (!File.Exists(_curDir + "/../../Data/rareoption.lod")) throw new FileNotFoundExceptionST("Отсутствует rareoption.lod", new Exception());
            if (!File.Exists(FILE_OPENED_ITEM_NAME)) throw new FileNotFoundExceptionST("Отсутствует strItem_ru.lod", new Exception());
            if (!File.Exists(_curDir + "/../../Local/ru/String/strOption_ru.lod")) throw new FileNotFoundExceptionST("Отсутствует strOption_ru.lod", new Exception());
            if (!File.Exists(_curDir + "/../../Local/ru/String/strRareOption_ru.lod")) throw new FileNotFoundExceptionST("Отсутствует strRareOption_ru.lod", new Exception());

            // Инициализация листов
            ITEM_ALL = new List<ItemAllLod>();
            OPTION = new List<OptionLod>();
            RARE = new List<RareOptionLod>();
            ITEM_NAME = new List<StrModel>();
            OPTION_NAME = new List<StrModel>();
            RARE_NAME = new List<StrModel>();

            // Открытие файлов
            bw_LoadFile.ReportProgress(0, "Загрузка itemAll.lod");
            ITEM_ALL = LodReader.ReadLod<ItemAllLod>(FILE_OPENED_ITEM_ALL);
                
            bw_LoadFile.ReportProgress(1, "Загрузка option.lod");
            OPTION = LodReader.ReadLod<OptionLod>(_curDir + "/../../Data/option.lod");
            //Thread.Sleep(500);
            bw_LoadFile.ReportProgress(2, "Загрузка rareoption.lod");
            RARE = LodReader.ReadLod<RareOptionLod>(_curDir + "/../../Data/rareoption.lod");
            //Thread.Sleep(1000);
            bw_LoadFile.ReportProgress(3, "Загрузка strItem_ru.lod");
            ITEM_NAME = StrLoader.LoadStringFile(StrFileType.ITEM, FILE_OPENED_ITEM_NAME);
            //Thread.Sleep(1000);
            bw_LoadFile.ReportProgress(4, "Загрузка strOption_ru.lod");
            OPTION_NAME = StrLoader.LoadStringFile(StrFileType.OPTION, _curDir + "/../../Local/ru/String/strOption_ru.lod");
            //Thread.Sleep(1000);
            bw_LoadFile.ReportProgress(5, "Загрузка strRareOption_ru.lod");
            RARE_NAME = StrLoader.LoadStringFile(StrFileType.RAREOPTION, _curDir + "/../../Local/ru/String/strRareOption_ru.lod");

            bw_LoadFile.ReportProgress(6, "Обработка данных");
            StrInItem();
        }

        public static void StrInItem(/*bool isusa*/)
        {
            List<StrModel> l = new List<StrModel>();
            List<StrModel> Item = new List<StrModel>();
            foreach (var mob in ITEM_ALL)
            {
                StrModel np = new StrModel();
                np.m_index = mob.ItemID;
                np.m_name = "";
                np.m_descs = new string[] { "" };
                foreach (var str in ITEM_NAME)
                {
                    if (mob.ItemID == str.m_index)
                    {
                        np.m_name = str.m_name;
                        np.m_descs = str.m_descs;
                        mob.Name = str.m_name;
                        mob.Desc = str.m_descs[0];
                        break;
                    }
                }
                Item.Add(np);
                //if (isusa)
                //{
                //    StrModel np2 = new StrModel();
                //    np2.m_index = mob.ItemID;
                //    np2.m_name = "GenNew Item";
                //    np2.m_descs = new string[] { "" };
                //    foreach (var str in strItemUS)
                //    {
                //        if (mob.ItemID == str.m_index)
                //        {
                //            np2.m_name = str.m_name;
                //            np2.m_descs = str.m_descs;
                //            break;
                //        }
                //    }
                //    l.Add(np2);
                //}
            }
            ITEM_NAME = Item;
            //if (isusa)
            //    strItemUS = l;
        }
    }
}
