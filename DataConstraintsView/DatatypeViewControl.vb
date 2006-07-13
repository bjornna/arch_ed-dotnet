'
'
'	component:   "openEHR Archetype Project"
'	description: "$DESCRIPTION"
'	keywords:    "Archetype, Clinical, Editor"
'	author:      "Sam Heard"
'	support:     "Ocean Informatics <support@OceanInformatics.biz>"
'	copyright:   "Copyright (c) 2004,2005,2006 Ocean Informatics Pty Ltd"
'	license:     "See notice at bottom of class"
'
'	file:        "$URL$"
'	revision:    "$LastChangedRevision$"
'	last_change: "$LastChangedDate$"
'
'

Public Class DatatypeViewControl : Inherits ElementViewControl 'Viewcontrol

    Private WithEvents mComboBox As ComboBox
    Private WithEvents mTextBox As TextBox
    Private WithEvents mNumeric As NumericUpDown
    Private WithEvents mListBox As ListBox

    Public Sub New(ByVal anElement As ArchetypeElement, ByVal a_filemanager As FileManagerLocal)
        MyBase.New(anElement, a_filemanager)

    End Sub

    Public Sub New(ByVal aConstraint As Constraint, a_filemanager as FileManagerLocal)
        MyBase.New(aConstraint, a_filemanager)

    End Sub

    Protected Overrides Sub InitialiseComponent(ByVal aConstraint As Constraint, _
            ByVal aLocation As System.Drawing.Point)

        Dim ctrl As Control = DataTypeToControl(aConstraint, aLocation)

        Me.Controls.Add(ctrl)
    End Sub

    Friend Function DataTypeToControl(ByVal aConstraint As Constraint, _
            ByRef Pos As Point) As Control

        Select Case aConstraint.Type

            Case ConstraintType.Any
                Dim lbl As New Label
                lbl.Height = 25
                lbl.Width = 70
                lbl.Text = "[Any]"
                lbl.Location = Pos

                Return lbl


            Case Else
                'Debug.Assert(False)
                Throw New NotSupportedException( _
                        String.Format("Constraint type '{0}' not supported as a view", _
                        aConstraint.Type.ToString))
        End Select
    End Function

    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles mComboBox.SelectedIndexChanged

        If mComboBox.SelectedValue Is Nothing Then
            Value = mComboBox.Text
        Else
            Value = CStr(mComboBox.SelectedValue)
        End If
    End Sub

    Private Sub TextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles mTextBox.TextChanged

        Value = mTextBox.Text
    End Sub

    Private Sub Numeric_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles mNumeric.ValueChanged

        Value = mNumeric.Value
    End Sub

    Private mValue As Object
    Public Overrides Property Value() As Object
        Get
            Return mValue
        End Get
        Set(ByVal Value As Object)
            mValue = Value
            MyBase.OnValueChanged()
        End Set
    End Property

    Private Sub ListBox_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles mListBox.SelectedValueChanged

        If mListBox.SelectedValue Is Nothing Then
            Value = mListBox.Text
        Else
            Value = CStr(mListBox.SelectedValue)
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
'The Original Code is DatatypeViewControl.vb.
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