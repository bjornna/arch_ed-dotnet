'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Option Explicit On 

Class Duration
    Private sISODuration As String
    Private sUnits As String
    Private iValue As Integer
    Private d As Integer
    Private h As Integer
    Private m As Integer
    Private s As Long

    Property ISO_duration() As String
        Get
            Return sISODuration
        End Get
        Set(ByVal Value As String)
            sISODuration = Value.Trim("| ".ToCharArray())
            ProcessIso()
        End Set
    End Property
    Property GUI_duration() As Integer
        Get
            Return iValue
        End Get
        Set(ByVal Value As Integer)
            If sUnits = "millisec" Then
                sISODuration = "P" & (Value / 1000).ToString & "s"
            Else
                sISODuration = "P" & Value.ToString & sUnits
            End If
            iValue = Value
        End Set
    End Property
    Property GUI_Units() As String
        Get
            Return sUnits
        End Get
        Set(ByVal Value As String)
            If sUnits <> Value Then
                sUnits = Value
                If sUnits = "millisec" Then
                    sISODuration = "P" & (iValue / 1000).ToString & "s"
                Else
                    sISODuration = "P" & iValue.ToString & sUnits
                End If
            End If
        End Set
    End Property

    Private Sub ProcessIso()
        Dim str As String
        Dim y() As String

        str = sISODuration.Substring(1) ' drop the leading P
        y = str.Split("d")
        If y.Length > 1 Then
            d = Val(y(0))
            str = y(1)
        End If
        y = str.Split("h")
        If y.Length > 1 Then
            h = Val(y(0))
            str = y(1)
        End If
        y = str.Split("m")
        If y.Length > 1 Then
            m = Val(y(0))
            str = y(1)
        End If
        y = str.Split("s")
        If y.Length > 1 Then
            s = Val(y(0))
        End If


        If sISODuration.EndsWith("d") Then
            sUnits = "day"
        ElseIf sISODuration.EndsWith("h") Then
            sUnits = "hr"
        ElseIf sISODuration.EndsWith("m") Then
            sUnits = "min"
        ElseIf sISODuration.EndsWith("s") Then
            If InStr(s.ToString, ".") > 0 Then
                ' this means there is a decimal point and the period must have been in millisecs
                If InStr(iValue.ToString, ".") Then
                    iValue = s * 1000
                    sUnits = "millisec"
                End If
            Else
                sUnits = "sec"
            End If
        End If


        If sUnits = "day" Then
            iValue = d
        ElseIf sUnits = "hr" Then
            iValue = (d * 24) + h

        ElseIf sUnits = "min" Then
            iValue = (((d * 24) + h) * 60) + m

        ElseIf sUnits = "sec" Then
            iValue = (((((d * 24) + h) * 60) + m) * 60) + s
        End If
    End Sub

End Class

'
'***** BEGIN LICENSE BLOCK *****
'Version: MPL 1.1/GPL 2.0/LGPL 2.1
'
'The contents of this file are subject to the Mozilla Public License Version 
'1.1 (the "License"); you may not use this file except in compliance with 
'the License. You may obtain a copy of the License at 
'http://www.mozilla.org/MPL/
'
'Software distributed under the License is distributed on an "AS IS" basis,
'WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
'for the specific language governing rights and limitations under the
'License.
'
'The Original Code is Duration.vb.
'
'The Initial Developer of the Original Code is
'Sam Heard, Ocean Informatics (www.oceaninformatics.biz).
'Portions created by the Initial Developer are Copyright (C) 2004
'the Initial Developer. All Rights Reserved.
'
'Contributor(s):
'	Heath Frankel
'
'Alternatively, the contents of this file may be used under the terms of
'either the GNU General Public License Version 2 or later (the "GPL"), or
'the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
'in which case the provisions of the GPL or the LGPL are applicable instead
'of those above. If you wish to allow use of your version of this file only
'under the terms of either the GPL or the LGPL, and not to allow others to
'use your version of this file under the terms of the MPL, indicate your
'decision by deleting the provisions above and replace them with the notice
'and other provisions required by the GPL or the LGPL. If you do not delete
'the provisions above, a recipient may use your version of this file under
'the terms of any one of the MPL, the GPL or the LGPL.
'
'***** END LICENSE BLOCK *****
'