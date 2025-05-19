using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VowelBonus.Shared.Common.Models
{
    public class  BaseConst
    {
        public const string GET_DATA_SUCCESS = "Get Data Successfully";
        public const string SAVE_SUCCESS = "Save Successfully";
        public const string UPDATE_SUCCESS = "Update Successfully";
        public const string DELETE_SUCCESS = "Delete Successfully";

        public const string NOT_FOUND = "Not Found!!";
        public const string ERROR_SUCCESS = "Save Error!!";
        public const string ERROR_EXIST_USER = "User Exist!!";
        public const string ERROR_EXIST_USER_NAME = "User Name Exist!!";
        public static readonly Dictionary<char, int> VOWEL_SCORES = new Dictionary<char, int>
        {
            { 'a', 2 },
            { 'e', 3 },
            { 'i', 5 },
            { 'o', 7 },
            { 'u', 9 }
        };

        public const int DEFAULT_TASK = 20;
    }
}
