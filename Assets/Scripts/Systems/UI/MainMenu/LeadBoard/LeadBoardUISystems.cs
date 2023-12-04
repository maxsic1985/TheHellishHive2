using Leopotam.EcsLite;



namespace HalfDiggers.Runner
{
    
    public class LeadBoardUISystems
    {
        
        public LeadBoardUISystems(EcsSystems systems)
        {
            systems
                .Add(new LeadBoardUILoadSystem())    // Добавляем основной компонент и имя аддресаблес файла
                .Add(new LeadBoardUIInitSystem())    //  Получаем префаб из скриптэйбла 
                .Add(new LeadBoardUIBuildSystem())   // Грузим GO на сцену
                
                .Add(new LeadBoardUICallBackSystem()) // Добавляем обработчики кнопок
                .Add(new LeadBoardUIHandSystem());    // Управление кнопками
        }
    }
}