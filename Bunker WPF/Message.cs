using System;
using System.Collections.Generic;
using System.Text;
using Bunker_WPF;

namespace Bunker
{
    //Класс, предназначенный для отображения прогресса игры (текстом, звуком или другими способами)
    //Реализация будет в будущем
    class Message
    {
        //Флаг доступа к делегатам
        public static bool FlagInfo { get; set; } = false;
        //Делегаты Info предназначены для отображения действий, происходящих "изнутри"
        public delegate void Info(string mes);
        public delegate void Info<T>(string mes, T pointer);

        //Флаг доступа к делегатам
        public static bool FlagGameInfo { get; set; } = false;
        //Делегаты GameInfo предназначены для отображения действий, происходящих во время игры
        public delegate void GameInfo(string mes);
        public delegate void GameInfo<T>(string mes, T pointer);


        
             
         
        
    }
}
