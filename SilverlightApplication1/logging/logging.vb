Imports System.IO.IsolatedStorage
Imports System.IO

Public Class logging
    Enum Level As Integer
        INFO = 0
        WARNING = 1
        ERR = 2
        FATAL = 3
    End Enum
    Public Sub Log(ByVal message As String, ByVal LogLEvel As Level)
        Exit Sub
        Try
            Using store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
                Using stream As Stream = New IsolatedStorageFileStream("Solution.Silverlight.log", FileMode.Append, FileAccess.Write, store)
                    Dim writer As StreamWriter = New StreamWriter(stream)
                    Select Case LogLEvel
                        Case Level.INFO
                            writer.Write(String.Format("{0:u} [INFO] {1}{2}", DateTime.Now, message, Environment.NewLine))
                        Case Level.WARNING
                            writer.Write(String.Format("{0:u} [WARNING] {1}{2}", DateTime.Now, message, Environment.NewLine))
                        Case Level.ERR
                            writer.Write(String.Format("{0:u} [ERROR] {1}{2}", DateTime.Now, message, Environment.NewLine))
                        Case Level.FATAL
                            writer.Write(String.Format("{0:u} [FATAL] {1}{2}", DateTime.Now, message, Environment.NewLine))
                    End Select
                    writer.Close()
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub
End Class
