// Machine generated C++ stub file (.h) for remote object MyService
// Created on : 10/03/2023 17:18:06


#ifndef _include_MyService_h
#define _include_MyService_h

#include <string>
#include <MarshallerCfg.h>
#include <Array.h>
#include <PCSC.h>
#include <Marshaller.h>

#ifdef MyService_EXPORTS
#define MyService_API __declspec(dllexport)
#else
#define MyService_API
#endif

using namespace std;
using namespace Marshaller;

class MyService_API MyService : private SmartCardMarshaller {
public:
	// Constructors
	MyService(string* uri);
	MyService(string* uri, u4 index);
	MyService(u2 portNumber, string* uri);
	MyService(u2 portNumber, string* uri, u4 index);
	MyService(string* readerName, string* uri);
	MyService(string* readerName, u2 portNumber, string* uri);
	MyService(SCARDHANDLE cardHandle, string* uri);
	MyService(SCARDHANDLE cardHandle, u2 portNumber, string* uri);

	// Pre-defined methods
	string* GetReader(void);

	// Exposed methods
	StringArray* ShowAllNumbers();
	StringArray* ShowAllNames();
	s4Array* ShowAllIds();
	s4 GetIndexSize();
	s4 GetOldIndexSize();
	s4 GetIdsArraySize();
	u1 GetDataDeletionPerformedValue();
	void ChangeValueOfRestoreBoolean();
	void AddNewAddress(string* name,string* number);
	void ReduseIndexByOneAfterDeletion();
	s4 ResetAllDataOnCard();
	s4 RestoreAllOldDataOnCard();
	void ReIndexArraysAfterDeletionOfItem(s4 positionOfElement);
	void ReIndexIdsArrayValuesAfterDeletionOfItem();
};


#endif
