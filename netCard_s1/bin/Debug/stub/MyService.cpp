// Machine generated C++ stub file (.cpp) for remote object MyService
// Created on : 10/03/2023 17:18:06

#ifdef WIN32
#include <windows.h>
#endif
#include <winscard.h>
#include "MyService.h"

using namespace std;
using namespace Marshaller;


// Constructors
MyService::MyService(string* uri) : SmartCardMarshaller(NULL, 0, uri, (u4)0x5865A1, (u2)0x09A7, 0) { return; }
MyService::MyService(string* uri, u4 index) : SmartCardMarshaller(NULL, 0, uri, (u4)0x5865A1, (u2)0x09A7, index) { return; }
MyService::MyService(u2 portNumber, string* uri) : SmartCardMarshaller(NULL, portNumber, uri, (u4)0x5865A1, (u2)0x09A7, 0) { return; }
MyService::MyService(u2 portNumber, string* uri, u4 index) : SmartCardMarshaller(NULL, portNumber, uri, (u4)0x5865A1, (u2)0x09A7, index) { return; }
MyService::MyService(string* readerName, string* uri) : SmartCardMarshaller(readerName, 0, uri, (u4)0x5865A1, (u2)0x09A7, 0) { return; }
MyService::MyService(string* readerName, u2 portNumber, string* uri) : SmartCardMarshaller(readerName, portNumber, uri, (u4)0x5865A1, (u2)0x09A7, 0) { return; }
MyService::MyService(SCARDHANDLE cardHandle, string* uri) : SmartCardMarshaller(cardHandle, 0, uri, (u4)0x5865A1, (u2)0x09A7) { return; }
MyService::MyService(SCARDHANDLE cardHandle, u2 portNumber, string* uri) : SmartCardMarshaller(cardHandle, portNumber, uri, (u4)0x5865A1, (u2)0x09A7) { return; }

// Pre-defined methods
std::string* MyService::GetReader(void){return GetReaderName();}

// Exposed methods

StringArray* MyService::ShowAllNumbers(){
	StringArray* _StringArray = NULL;
	Invoke(0, 0xBF62, MARSHALLER_TYPE_RET_STRINGARRAY, &_StringArray);
	return _StringArray;
}


StringArray* MyService::ShowAllNames(){
	StringArray* _StringArray = NULL;
	Invoke(0, 0xFC12, MARSHALLER_TYPE_RET_STRINGARRAY, &_StringArray);
	return _StringArray;
}


s4Array* MyService::ShowAllIds(){
	s4Array* _s4Array = NULL;
	Invoke(0, 0x4B92, MARSHALLER_TYPE_RET_S4ARRAY, &_s4Array);
	return _s4Array;
}


s4 MyService::GetIndexSize(){
	s4 _s4 = 0;
	Invoke(0, 0x7EF4, MARSHALLER_TYPE_RET_S4, &_s4);
	return _s4;
}


s4 MyService::GetOldIndexSize(){
	s4 _s4 = 0;
	Invoke(0, 0xE9DB, MARSHALLER_TYPE_RET_S4, &_s4);
	return _s4;
}


s4 MyService::GetIdsArraySize(){
	s4 _s4 = 0;
	Invoke(0, 0x42CD, MARSHALLER_TYPE_RET_S4, &_s4);
	return _s4;
}


u1 MyService::GetDataDeletionPerformedValue(){
	u1 _u1 = 0;
	Invoke(0, 0x045B, MARSHALLER_TYPE_RET_BOOL, &_u1);
	return _u1;
}


void MyService::ChangeValueOfRestoreBoolean(){
	Invoke(0, 0xB572, MARSHALLER_TYPE_RET_VOID);
}


void MyService::AddNewAddress(string* name,string* number){
	Invoke(2, 0xEA54, MARSHALLER_TYPE_IN_STRING, name, MARSHALLER_TYPE_IN_STRING, number, MARSHALLER_TYPE_RET_VOID);
}


void MyService::ReduseIndexByOneAfterDeletion(){
	Invoke(0, 0x5C59, MARSHALLER_TYPE_RET_VOID);
}


s4 MyService::ResetAllDataOnCard(){
	s4 _s4 = 0;
	Invoke(0, 0x050B, MARSHALLER_TYPE_RET_S4, &_s4);
	return _s4;
}


s4 MyService::RestoreAllOldDataOnCard(){
	s4 _s4 = 0;
	Invoke(0, 0xDA04, MARSHALLER_TYPE_RET_S4, &_s4);
	return _s4;
}


void MyService::ReIndexArraysAfterDeletionOfItem(s4 positionOfElement){
	Invoke(1, 0x11BA, MARSHALLER_TYPE_IN_S4, positionOfElement, MARSHALLER_TYPE_RET_VOID);
}


void MyService::ReIndexIdsArrayValuesAfterDeletionOfItem(){
	Invoke(0, 0xCD40, MARSHALLER_TYPE_RET_VOID);
}



