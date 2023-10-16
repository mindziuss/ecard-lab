
using System;

namespace MyCompany.MyOnCardApp
{
    /// <summary>
    /// Summary description for MyService.
    /// </summary>
    public class MyService : MarshalByRefObject
    {
        //arrays for data.
        string[] numbersArray = new string[10];
        string[] namesArray = new string[10];
        int[] idsArray = new int[10];

        //deleted arrays for restoration.
        string[] numbersArrayOld = new string[10];
        string[] namesArrayOld = new string[10];
        int[] idsArrayOld = new int[10];

        //data deletion boolean to keep track of deletion.
        bool dataDeletionWasPerformed = false;

        //index count.
        int addressIndex = 0;

        //index for restoration.
        int oldAddressIndex = 0;

        public string[] ShowAllNumbers()
        {
            return numbersArray;
        }

        public string[] ShowAllNames()
        {
            return namesArray;
        }

        public int[] ShowAllIds()
        {
            return idsArray;
        }

        public int GetIndexSize()
        {
            return addressIndex;
        }

        public int GetOldIndexSize()
        {
            return oldAddressIndex;
        }

        public int GetIdsArraySize()
        {
            return idsArray.Length;
        }

        public bool GetDataDeletionPerformedValue()
        {
            return dataDeletionWasPerformed;
        }

        public void ChangeValueOfRestoreBoolean()
        {
            dataDeletionWasPerformed = !dataDeletionWasPerformed;
        }

        public void AddNewAddress(string name, string number)
        {
            idsArray.SetValue(addressIndex, addressIndex);
            namesArray.SetValue(name, addressIndex);
            numbersArray.SetValue(number, addressIndex);

            addressIndex++;
        }

        public void ReduseIndexByOneAfterDeletion()
        {
           if (addressIndex == 0) {
             addressIndex = 0;
           } else {
              addressIndex--;
           } 
        }

        public int ResetAllDataOnCard() 
        {
            if (dataDeletionWasPerformed) {
                return 8;
            } else {
                //Save values for restoration.
                numbersArrayOld = numbersArray;
                namesArrayOld = namesArray;
                idsArrayOld = idsArray;
                oldAddressIndex = addressIndex;

                //Reset values.
                numbersArray = new string[10];
                namesArray = new string[10];
                idsArray = new int[10];
                addressIndex = 0;

                dataDeletionWasPerformed = true;
            }

            return 108;
        }

        public int RestoreAllOldDataOnCard()
        {
            if (dataDeletionWasPerformed) {
                //Restore values.
                numbersArray = numbersArrayOld;
                namesArray = namesArrayOld;
                idsArray = idsArrayOld;
                addressIndex = oldAddressIndex;

                //Reset restoration arrays to empty.
                numbersArrayOld = new string[10];
                namesArrayOld = new string[10];
                idsArrayOld = new int[10];
                oldAddressIndex = 0;

                dataDeletionWasPerformed = false;

                return 109;
            } else {
                return 9;
            }
        }

        public void ReIndexArraysAfterDeletionOfItem(int positionOfElement)
        {
            for (int i = positionOfElement + 1; i < idsArray.Length; i++) {
                idsArray[i - 1] = idsArray[i];
                namesArray[i - 1] = namesArray[i];
                numbersArray[i - 1] = numbersArray[i];
            }

            idsArray[idsArray.Length - 1] = 0;
            namesArray[namesArray.Length - 1] = "";
            numbersArray[numbersArray.Length - 1] = "";
        }

        public void ReIndexIdsArrayValuesAfterDeletionOfItem()
        {
            int index = 0;

            foreach(int id in idsArray)
            {
               if (id != 0) {
                    idsArray[index] = id - 1;
               }

               index++;
            }
        }

    }
}

