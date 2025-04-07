namespace IntelChat.Data
{
    public static class SlotConvert
    {
        public static string ToTimeStr(int SlotSelection)
        {
            string minute = SlotSelection % 2 == 1 ? "00" : "30";
            int hour = (SlotSelection - 1) / 2;

            return $"{hour}:{minute}";
        }
    }
}
