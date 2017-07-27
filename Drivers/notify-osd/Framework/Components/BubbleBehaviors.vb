Public Enum BubbleBehaviors
    ''' <summary>
    ''' The message bubble will be closed automatically after a time period.
    ''' </summary>
    AutoClose
    ''' <summary>
    ''' The message bubble will not be closed until user click on it.
    ''' </summary>
    FreezUntileClick
    ''' <summary>
    ''' The message bubble will running as a process bar indicator, the bubble will automatically closed when the process value is 100.
    ''' </summary>
    ProgressIndicator
    ''' <summary>
    ''' When the user scrolling his mouse wheel on the bubble, then the action will be callback to adjust the value.
    ''' </summary>
    ValueAdjustments
End Enum