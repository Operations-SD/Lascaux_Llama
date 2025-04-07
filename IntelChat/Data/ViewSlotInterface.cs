using IntelChat.Models;
using System.Linq;

namespace IntelChat.Data
{
    public class ViewSlotInterface
    {
        public List<ViewSlot> SlotList;
        public int Pos { get; set; }
        public int DayLowestIndex { get; set; }
        public int DaySelection { get; set; }
        public int Count
        {
            get { return SlotList.Count;}
        }

        //Note - It is inefficient to pass the full list around to initialize each of these. A better data structure exists.
        public ViewSlotInterface(List<ViewSlot> slotListInput, int daySelection)
        {
            SlotList = new List<ViewSlot>();
            DayLowestIndex = -1;    // -1 = There are no time slots for this day. This will be changed to an int >=0 if there are any time slots for that day
            DaySelection = daySelection;
            setDay(slotListInput, daySelection);   //(int) 1 = Sunday = Default day of week
            Pos = 0;
        }

        public void setDay(List<ViewSlot> slotListInput, int daySelection)
        {
            for (int i = 0; i < slotListInput.Count; i++)
            {   //Search for the first list entry which matches the desired day of the week.
                if (slotListInput[i].WeekDay == daySelection)
                {
                    DayLowestIndex = i;
                    for (int j = i; j < (slotListInput.Count); j++)
                    {   //Continue adding entries to this class's sub-list until finding one of another day of the week (or the end of the parent-list)
                        if (slotListInput[j].WeekDay == daySelection)
                        {
                            //This code WILL BREAK after implementing Create functionality into UI
                            SlotList.Add(slotListInput[j]);
                        }
                        else
                            break;
                    }
                    break;
                }
            }
        }

        public ViewSlot Next(bool peek = false)
        {
            int LocalPos = Pos;
            LocalPos++;
            if (LocalPos >= SlotList.Count)
                LocalPos = 0;

            if (DayLowestIndex == -1)
            {
                return null;
            }
            else if (!peek)
            {
                Pos = LocalPos;
                return Current();
            }
            else
            {
                return SlotList[LocalPos];
            }
        }

        public ViewSlot Prior(bool peek = false)
        {
            int LocalPos = Pos;
            LocalPos--;
            if (LocalPos < 0)
                LocalPos = SlotList.Count - 1;

            if (DayLowestIndex == -1)
            {
                return null;
            }
            else if (!peek)
            {
                Pos = LocalPos;
                return Current();
            }
            else
            {
                return SlotList[LocalPos];
            }
        }

        public ViewSlot Current()
        {
            if (DayLowestIndex != -1)
                return SlotList[Pos];
            else
                return null;
        }
    }
}
