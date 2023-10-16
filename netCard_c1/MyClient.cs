
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using SmartCard.Runtime.Remoting.Channels.APDU;

// make sure you add the reference to your server stub dll or interface
// The stub file is automatically generated for you, under [Server Project Output]\Stub).
using MyCompany.MyOnCardApp;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Sockets;
using System.Drawing.Text;
using System.Drawing;
using System.Linq.Expressions;

namespace MyCompany.MyClientApp
{
    /// <summary>
    /// Summary description for MyClient.
    /// </summary>
    public class MyClient
    {
        //OPERATIONS CONSTANTS
        private const int OP_STOP_WORK = 0;
        private const int OP_SHOW_ALL_ADDRESSES = 1;
        private const int OP_ADD_NEW_ADDRESS = 2;
        private const int OP_REMOVE_ADDRESS = 3;
        private const int OP_REMOVE_ALL_ADDRESSES = 4;
        private const int OP_RESTORE_DELETED_ADDRESSES = 5;
        private const int TOTAL_OPS = 5;
        //COMMON CONSTANTS
        private const string URL = "apdu://selfdiscover/MyService.uri";
        private const string PROGRAM_AUTHOR = "Atliko : Mindaugas Valiokas IFM 3/3";
        private const string NAME_INPUT = "Áveskite adreso vardà";
        private const string NUMBER_INPUT = "Áveskite adreso numerá";
        private const string OPERATION_STRING_IS_NOT_NUMBER = "Áveskite operacijos skaièiø 0-5";
        private const string ID_STRING_IS_NOT_NUMBER = "Áveskite adreso id, sveikas skaièius..";
        private const string ID_TO_REMOVE_FROM_ADDRESS_LIST = "Áveskite adreso, kurá norite iðtrinti ID : ";
        private const string ADD_ADDRESS_BODY = "Pasirinkote operacijà pridëti adresà.";
        private const string REMOVE_ADDRESS_BODY = "Pasirinkote operacijà iðtrinti adresà.";
        private const string PREVIEW_ALL_ADDRESSES_BODY = "Pasirinkote operacijà perþiûrëti adresus.";
        private const string REMOVE_ALL_ADDRESSES_BODY = "Pasirinkote operacijà iðtrinti visus adresus";
        private const string RESTORE_ALL_ADDRESSES_BODY = "Pasirinkote operacijà atstatyti visus adresus";
        private const string ADDRESSES_WERE_DELETED = "Adresai jau buvo iðtrinti, jeigu iðtrinsite negalësite atstatyti senø duomenø";
        private const string DO_YOU_WANT_TO_CONTINUE = "Ar tikrai norite tæsti? 1 - Taip, 0 - Ne";
        private const string DELETION_PERFORMED = "Trynimas atlikas! Norint atstatyti naudokitës operacija 5";
        private const string ATTENTION_RESTORE_WILL_OVERWRITE = "Dëmesio! Atstatymas perraðys dabartinæ adresø knygutæ!";
        private const string RESTORE_PERFORMED = "Atstatymas atlikas!";
        private const string INSERT_CARD_AND_PRESS_YES = "Idëjus kortelæ paspauskite 1 - Jeigu norite paleisti aplikacijà ið naujo, 0 - Uþdaryti";
        private const string ID = "ID : ";
        private const string NAME = "Vardas : ";
        private const string NUMBER = "Numeris : ";
        private const string ADD_ADDRESS = "Pridëti adresà : ";
        private const string REMOVE_ADDRESS = "Iðtrinti adresà : ";
        private const string PREVIEW_ADDRESSES = "Perþiûrëti visus adresus : ";
        private const string DELETE_ALL_ADDRESSES = "Iðtrinti visus adresus : ";
        private const string RESTORE_ALL_ADDRESSES = "Atstatyti iðtrintus adresus : ";
        private const string STOP_PROGRAM = "Baigti darbà : ";
        private const string CHOOSE_OP = "|  Pasirinkite operacijà : ";
        private const string TOTAL_ADDRESSES_ON_BOOK = "Iðviso adresø knygutëje yra {0}/{1} adresø.";

        //ERROR CODES
        private const string ERROR_1 = "Klaida nr. 1 : Tuðèia eilutë.";
        private const string ERROR_2 = "Klaida nr. 2 : Per daug simboliø.";
        private const string ERROR_3 = "Klaida nr. 3 : Tokia operacija neegzistuoja, rinkitës nuo 0 - 5.";
        private const string ERROR_4 = "Klaida nr. 4 : Tokio id nëra rinkitës nuo 0 - 9.";
        private const string ERROR_5 = "Klaida nr. 5 : Adresø knygutë pilna, norint pridëti iðtrinkite adresà.";
        private const string ERROR_6 = "Klaida nr. 6 : Tokio adreso nëra.";
        private const string ERROR_7 = "Klaida nr. 7 : Adresø trynimas nebuvo atliktas, todël nëra kà atsatyti.";
        private const string ERROR_7_1 = "Arba nëra duomenø kuriuos bûtø galima atstatyti";
        private const string ERROR_8 = "Klaida nr. 8 Kortelë buvo iðtraukta, ádëkite norint naudotis sistema.";
        private const string ERROR_STACK_TRACE = "Stack trace pateiktas þemiau : ";

        public static void Main()
        {
            // create and register communication channel
            APDUClientChannel channel = new APDUClientChannel();
            ChannelServices.RegisterChannel(channel);

            // get the referenc to remote object
            MyService service = (MyService)Activator.GetObject(typeof(MyService), URL);

            bool runApp = true;
            int inputValue;
            string name, number;
            
            try {
                while (runApp) {
                    //Some of the values USED
                    int indexOnCardSize = service.GetIndexSize();
                    int idsArraySize = service.GetIdsArraySize();
                    string[] names = service.ShowAllNames();
                    string[] numbers = service.ShowAllNumbers();
                    int[] ids = service.ShowAllIds();
                    int totalAddresses = idsArraySize;
                    int takenAddresses = indexOnCardSize;
                    //----------------------------

                    OperationBody(PROGRAM_AUTHOR, ConsoleColor.DarkMagenta);
                    ProgramMenuBody();

                    inputValue = ConfirmIntAndFormMessageResponse(TOTAL_OPS, ERROR_3, OPERATION_STRING_IS_NOT_NUMBER);

                    if (inputValue == OP_STOP_WORK) {
                        Environment.Exit(0);
                    }

                    if (inputValue == OP_SHOW_ALL_ADDRESSES) {
                        PrintAddressInfo(names, numbers, ids, totalAddresses, takenAddresses);

                        continue;
                    }

                    if (inputValue == OP_ADD_NEW_ADDRESS) {

                        if (indexOnCardSize == idsArraySize) {
                            ProgramErrorBody(ERROR_5, true);

                            continue;
                        }

                        OperationBody(ADD_ADDRESS_BODY, ConsoleColor.DarkYellow);

                        name = checkStringAndValidateItInTheEndReturnIt(NAME_INPUT);
                        number = checkStringAndValidateItInTheEndReturnIt(NUMBER_INPUT);

                        service.AddNewAddress(name, number);

                        Console.Clear();

                        continue;
                    }

                    if (inputValue == OP_REMOVE_ADDRESS) {
                        OperationBody(REMOVE_ADDRESS_BODY, ConsoleColor.DarkRed);
                        Console.WriteLine(ID_TO_REMOVE_FROM_ADDRESS_LIST);

                        int idToDelete = ConfirmIntAndFormMessageResponse(idsArraySize - 1, ERROR_4, ID_STRING_IS_NOT_NUMBER);

                        if ((indexOnCardSize < idToDelete) || (indexOnCardSize == 0)) {
                            ProgramErrorBody(ERROR_6, true);

                            continue;
                        } else {
                            service.ReIndexArraysAfterDeletionOfItem(idToDelete);
                            service.ReIndexIdsArrayValuesAfterDeletionOfItem();
                            service.ReduseIndexByOneAfterDeletion();
                        }

                        Console.Clear();

                        continue;
                    }

                    if (inputValue == OP_REMOVE_ALL_ADDRESSES) {
                        OperationBody(REMOVE_ALL_ADDRESSES_BODY, ConsoleColor.Red);
                        ResetAllAddresses(service.ResetAllDataOnCard(), service);

                        continue;
                    }

                    if (inputValue == OP_RESTORE_DELETED_ADDRESSES) {
                        OperationBody(RESTORE_ALL_ADDRESSES_BODY, ConsoleColor.DarkYellow);
                        OperationBody(ATTENTION_RESTORE_WILL_OVERWRITE, ConsoleColor.Red);

                        RestoreAllAddresses(service.RestoreAllOldDataOnCard(), service);

                        continue;
                    }

                    Console.Read();
                }
            } catch (Exception e) {
                CardRemovalError(e);
            }
            // unregister the communication channel
            ChannelServices.UnregisterChannel(channel);

            return;
        }

        public static void CardRemovalError(Exception e)
        {
            ProgramErrorBody(ERROR_8, true);
            ProgramErrorBody(ERROR_STACK_TRACE, false);

            Console.WriteLine(e.ToString());

            OperationBody(INSERT_CARD_AND_PRESS_YES, ConsoleColor.Cyan);

            while (true) {
               int inputValue = ConfirmIntAndFormMessageResponse(1, INSERT_CARD_AND_PRESS_YES, INSERT_CARD_AND_PRESS_YES);

                if (inputValue == 0) {
                    Environment.Exit(0);
                }
                else {
                    Application.Restart();
                    Environment.Exit(0);
                }

                Console.Read();
            }
        }

        public static int CheckLenghtOfString(string str)
        {
            //Empty string Error Code 1
            if (str.Length == 0)
            {
                return 1;
            }
            //Too long Error code 2
            if (str.Length > 20)
            {
                return 2;
            }

            return 100;
        }

        public static int checkOperationInputAndReturnSelectedOP(string errorString)
        {
            int numericInput;

            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input) || !Char.IsDigit(input, 0)) {
                    ProgramErrorBody(errorString, true);
                    ProgramMenuBody();
                } else {
                    numericInput = Int32.Parse(input);
                    break;
                }
            }

            return numericInput;
        }

        public static string checkStringAndValidateItInTheEndReturnIt(string nameOfInput)
        {
            string tempString;

            while (true){
                Console.WriteLine(nameOfInput);

                string inputString = Console.ReadLine();

                if (CheckLenghtOfString(inputString) == 1)
                {
                    ProgramErrorBody(ERROR_1, true );

                    continue;
                }

                if (CheckLenghtOfString(inputString) == 2)
                {
                    ProgramErrorBody(ERROR_2, true);

                    continue;
                }

                if (CheckLenghtOfString(inputString) == 100)
                {
                    tempString = inputString;
                    break;
                }
            }

            return tempString;
        }

        public static bool IntIsInsideSelectedBonds(int IntToCompare, int maxIntValue)
        {
            if (IntToCompare >= 0 && IntToCompare <= maxIntValue)
            {
                return true;
            }
           
            return false;
        }

        public static int ConfirmIntAndFormMessageResponse(int maxIntValue, string errorBody, string errorBodyForStringValidation) 
        {
            int inputValue = checkOperationInputAndReturnSelectedOP(errorBodyForStringValidation);

            while (true) {

                if (IntIsInsideSelectedBonds(inputValue, maxIntValue)) {
                    break;
                } else {
                    ProgramErrorBody(errorBody, true);
                    ProgramMenuBody();

                    inputValue = checkOperationInputAndReturnSelectedOP(errorBodyForStringValidation);
                }   
            }

            return inputValue;
        }

        public static void ResetAllAddresses(int valueFromFunction, MyService service)
        {
            if (valueFromFunction == 8) {
                ProgramErrorBody(DO_YOU_WANT_TO_CONTINUE, false);

                int inputVal = ConfirmIntAndFormMessageResponse(1, ADDRESSES_WERE_DELETED, DO_YOU_WANT_TO_CONTINUE);

                if (inputVal == 0) {
                    return;
                } else {
                    service.ChangeValueOfRestoreBoolean();
                    service.ResetAllDataOnCard();
                    OperationBody(DELETION_PERFORMED, ConsoleColor.White);
                }
            }

            if (valueFromFunction == 108) {
                OperationBody(DELETION_PERFORMED, ConsoleColor.White);
            }
        }

        public static void RestoreAllAddresses(int valueFromFunction, MyService service)
        {
            if (valueFromFunction == 9) {
                ProgramErrorBody(DO_YOU_WANT_TO_CONTINUE, false);

                int inputVal = ConfirmIntAndFormMessageResponse(1, ATTENTION_RESTORE_WILL_OVERWRITE, DO_YOU_WANT_TO_CONTINUE);

                if (inputVal == 0) {
                    return;
                }
                else {
                    service.ChangeValueOfRestoreBoolean();
                    service.RestoreAllOldDataOnCard();
                    OperationBody(RESTORE_PERFORMED, ConsoleColor.White);
                }
            }

            if (valueFromFunction == 109) {
                ProgramErrorBody(DO_YOU_WANT_TO_CONTINUE, false);

                int inputVal = ConfirmIntAndFormMessageResponse(1, ATTENTION_RESTORE_WILL_OVERWRITE, DO_YOU_WANT_TO_CONTINUE);

                if (inputVal == 0)
                {
                    return;
                }
                else
                {
                    service.RestoreAllOldDataOnCard();
                    OperationBody(RESTORE_PERFORMED, ConsoleColor.White);
                }
            }
        }

        public static void PrintAddressInfo(string[] names, string[] numbers, int[] ids, int totalAddresses, int takenAddresses) {
            int index = 0;
            string tempStringForTotalAddresses;

            tempStringForTotalAddresses = String.Format(TOTAL_ADDRESSES_ON_BOOK,
                takenAddresses, totalAddresses);
            
            Console.Clear();

            OperationBody(PREVIEW_ALL_ADDRESSES_BODY, ConsoleColor.DarkYellow);
            OperationBody(tempStringForTotalAddresses, ConsoleColor.DarkCyan);

            if (takenAddresses == 0) {
                return;
            }

            while  (index < ids.Length) {
                
                int id = ids[index];
                string name = names[index];
                string number = numbers[index];

                if ((index == 0 && id == 0) || id > 0) {
                    Console.Write(ID);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(id);

                    Console.ResetColor();
                    Console.Write(NAME);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(name);

                    Console.ResetColor();
                    Console.Write(NUMBER);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(number);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("------------------------------------------");
                    Console.ResetColor();
                }

                index++;
            }
        }

        public static void ProgramMenuBody()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-------------------------------");
            Console.WriteLine(CHOOSE_OP);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(PREVIEW_ADDRESSES);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(OP_SHOW_ALL_ADDRESSES);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(ADD_ADDRESS);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(OP_ADD_NEW_ADDRESS);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(REMOVE_ADDRESS);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(OP_REMOVE_ADDRESS);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(DELETE_ALL_ADDRESSES);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(OP_REMOVE_ALL_ADDRESSES);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(RESTORE_ALL_ADDRESSES);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(OP_RESTORE_DELETED_ADDRESSES);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("|  ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(STOP_PROGRAM);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(OP_STOP_WORK);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-------------------------------");
            Console.ResetColor();
        }

        public static void OperationBody(string body, ConsoleColor bodyForeground) 
        { 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("------------------------------------------");
            Console.Write("| ");
            Console.ForegroundColor = bodyForeground;
            Console.Write(body);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" |");
            Console.WriteLine("------------------------------------------");
            Console.ResetColor();
        }

        public static void ProgramErrorBody(string body, bool clearConsole)
        {
            if (clearConsole) {
                Console.Clear();
            }
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("------------------------------------------");
            Console.Write("| ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(body);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" |");
            Console.WriteLine("------------------------------------------");
            Console.ResetColor();
        }
    }
}

