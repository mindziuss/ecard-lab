
// ---------------------------------------MyService.js
SConnect.RegisterNamespace("MyCompany.MyOnCardApp");

MyCompany.MyOnCardApp.MyService = function(readerName,portNum,serviceName){
	this.marshaller = new SConnect.Marshaller(readerName,portNum,serviceName,0x5865A1, 0x09A7);
}

MyCompany.MyOnCardApp.MyService.prototype = {

	ShowAllNumbers : function(){
		return this.marshaller.invoke(0, 0xBF62, MARSHALLER_TYPE_RET_STRINGARRAY);
	},

	ShowAllNames : function(){
		return this.marshaller.invoke(0, 0xFC12, MARSHALLER_TYPE_RET_STRINGARRAY);
	},

	ShowAllIds : function(){
		return this.marshaller.invoke(0, 0x4B92, MARSHALLER_TYPE_RET_S4ARRAY);
	},

	GetIndexSize : function(){
		return this.marshaller.invoke(0, 0x7EF4, MARSHALLER_TYPE_RET_S4);
	},

	GetOldIndexSize : function(){
		return this.marshaller.invoke(0, 0xE9DB, MARSHALLER_TYPE_RET_S4);
	},

	GetIdsArraySize : function(){
		return this.marshaller.invoke(0, 0x42CD, MARSHALLER_TYPE_RET_S4);
	},

	GetDataDeletionPerformedValue : function(){
		return this.marshaller.invoke(0, 0x045B, MARSHALLER_TYPE_RET_BOOL);
	},

	ChangeValueOfRestoreBoolean : function(){
		this.marshaller.invoke(0, 0xB572, MARSHALLER_TYPE_RET_VOID);
	},

	AddNewAddress : function(name,number){
		this.marshaller.invoke(2, 0xEA54, MARSHALLER_TYPE_IN_STRING, name, MARSHALLER_TYPE_IN_STRING, number, MARSHALLER_TYPE_RET_VOID);
	},

	ReduseIndexByOneAfterDeletion : function(){
		this.marshaller.invoke(0, 0x5C59, MARSHALLER_TYPE_RET_VOID);
	},

	ResetAllDataOnCard : function(){
		return this.marshaller.invoke(0, 0x050B, MARSHALLER_TYPE_RET_S4);
	},

	RestoreAllOldDataOnCard : function(){
		return this.marshaller.invoke(0, 0xDA04, MARSHALLER_TYPE_RET_S4);
	},

	ReIndexArraysAfterDeletionOfItem : function(positionOfElement){
		this.marshaller.invoke(1, 0x11BA, MARSHALLER_TYPE_IN_S4, positionOfElement, MARSHALLER_TYPE_RET_VOID);
	},

	ReIndexIdsArrayValuesAfterDeletionOfItem : function(){
		this.marshaller.invoke(0, 0xCD40, MARSHALLER_TYPE_RET_VOID);
	},

	dispose : function(){
		this.marshaller.dispose();
	}

};
