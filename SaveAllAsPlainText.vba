'Word macro to save all docs in a folder as plain text
'Generated by ChatGPT 04/30/2023
Sub SaveAllAsPlainText()
    Dim strFolder As String
    Dim strFileName As String
    Dim objDoc As Document
    
    'Specify the folder path where the Word documents are stored
    strFolder = "C:\Users\wendt\OneDrive - American Red Cross\TrainingData\"
    
    'Get the first file in the folder
    strFileName = Dir(strFolder & "*.doc*")
    
    'Cycle through all Word documents in the folder
    Do While strFileName <> ""
        'Open the Word document
        Set objDoc = Documents.Open(strFolder & strFileName)
        
        'Save the Word document as plain text
        objDoc.SaveAs2 FileName:=strFolder & Left(strFileName, Len(strFileName) - 4) & ".txt", FileFormat:=wdFormatText, Encoding:=65001
        
        'Close the Word document
        objDoc.Close
        
        'Get the next file in the folder
        strFileName = Dir()
    Loop
    
    'Display a message when the task is complete
    MsgBox "All Word documents in the folder have been saved as plain text."
End Sub

