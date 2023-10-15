
Imports System.IO.Ports

Public Module MThermostat
    Public Const PRODUCT_THERMOSTAT_T2000 As Byte = &H0
End Module



Public MustInherit Class CThermostat
    Inherits CProduct

    Public Sub New(ByRef NewOwner As Form, ByRef NewPort As SerialPort, NewProductID As Byte)
        MyBase.New(NewOwner, NewPort, FAMILY_THERMOSTAT, NewProductID)
    End Sub
End Class
