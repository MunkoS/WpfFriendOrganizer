   M S B A M L   `   `   `  ����M  IFriendOrganizer.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:FriendOrganizer.UI.ViewFriendOrganizer.UI.View  5      K*clr-namespace:FriendOrganizer.UI.ViewModelFriendOrganizer.UI.ViewModel  5      ,    &FriendOrganizer.UI.View.ListDetailView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:FriendOrganizer.UI.View     5      @	viewModel*clr-namespace:FriendOrganizer.UI.ViewModel     5      .��5
      �   � RowDefinitions  5   
   �� 5      �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      �� 5      $
��Auto �6   .+�5   
   &   System.Windows.Controls.DataGrid     AutoGenerateColumns .     RowHeaderWidth$ 0Q�5   4   $��10q�6G   ��5      �� *
Properties+ �� SelectedItem 5      �� *SelectedProperty+ �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 67      DataGridColumnStyle1  +System.Windows.Controls.DataGridBoundColumn	   ElementStyle#	 [  5        DataGridEditingColumnStyle
   EditingElementStyle#
 [ 5         Binding 5      �� *Name+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5   
   $��1��6   $��0 5 5 5q�6%   .+�5       �� $��Добавить��6   $	��125��6(   $��5q�64   ��6?   �� *
AddCommand+�� 5!      $��Удалить��6   $	��125��6'   $��5q�63   ��6>   �� *RemoveCommand+5"      �� 5#   
   $��2��6    �� Orientation$ 
Horizontal=�6"   .+�5$      �� $��Сохранить��6   $	��125��6)   $��10q�65   ��6A   �� *SaveCommand+5'      5(      5)      