using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ToDoList
{
    public class DataItem
    {
        public enum ItemStatus
        {
            None = -1,
            Active = 0,
            Done = 1,
            Canceled = 2
        }
        public int id = -1;
        public ItemStatus status = ItemStatus.None;
        public DateTime created = DateTime.Now;
        public string title = string.Empty;
        public DataItem(int id, ItemStatus status, DateTime created, string title)
        {
            this.id = id;
            this.status = status;
            this.created = created;
            this.title = title;
        }
        public static ItemStatus GetStatusByID(int id)
        {
            switch (id)
            {
                case 1: return ItemStatus.Active;
                case 2: return ItemStatus.Done;
                case 3: return ItemStatus.Canceled;
                default: return ItemStatus.None;
            }
        }
    }
}