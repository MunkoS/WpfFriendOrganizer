   M S B A M L   `   `   `  ����P  LFriendOrganizer.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\Bclr-namespace:FriendOrganizer.Model;assembly=FriendOrganizer.ModelFriendOrganizer.Model  5      M IFriendOrganizer.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:FriendOrganizer.UI.ViewFriendOrganizer.UI.View 5      /   )FriendOrganizer.UI.View.MeetingDetailView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      TmodelBclr-namespace:FriendOrganizer.Model;assembly=FriendOrganizer.Model     5      7local%clr-namespace:FriendOrganizer.UI.View     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� $%��Название встречи��6   $��
10 10 10 0q�6+   �� 5   
   $��1��6   $��
10 0 10 10q�5   ;   ��5      �� *Meeting.Title+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5   ;   �� 5   
   $��2��6   $��Начало��6   $
��10 0q�6.   (  "System.Windows.Controls.DatePicker  5   
   $��3��6   $��
10 0 10 10q�5   ;      SelectedDate 5   "   �� *Meeting.DateFrom+$ TwoWay=�$ PropertyChanged=�5   ;   �� 5   
   $��4��6   $��
Конец��6   $
��10 0q�6-     5    
   $��5��6   $��
10 0 10 10q�5!       5    "   �� *Meeting.DateTo+$ PropertyChanged=�$ TwoWay=�5!      � 5#   
   $��6��6   $
��10 0q�6    � 	Resources 5$       �� DataType"   FriendOrganizer.Model.Friend��%�   (��       5%      *, +)�� 6   "  6   .R�5&      �� �� 6!   �6%   �� *	FirstName+�� 6B   �6F   �� *
LastName+6c   5'      5(       � ColumnDefinitions 5)      �� 5*      �� 5+      $
��Auto �6#   �� 5,        5.      �� 5/      $
��Auto �6    �� 50      .+�54   
   �� $��0��6   $)��#Добавленные друзья��6   �� 55   
   $��1��6   ��6    �� *AddedFriends+ �� SelectedItem 56      �� *SelectedAddedFriend+�� 58   
   $��1��6   $��1��6"   .+�59      �� $��<<��6   $��10q�6(   ��64   �� *AddFriendCommand+�� 5:      $��>>��6   $��10q�6(   ��64   �� *RemoveFriendCommand+5;      �� 5<      $��0��6   $��2��6!   $.��(Не добавленные друзья��61   �� 5=      $��1��6   $��2��6$   ��64   �� *AvailableFriends+ 5>      �� *SelectedAvailableFriend+5?      �� 5@   
   $��9��6    �� Orientation$ 
Horizontal=�6"   .+�5A      �� $��Сохранить��6   $	��125��6)   $��10q�65   ��6A   �� *SaveCommand+�� 5B      $��Удалить��6   $	��125��6'   $��10q�63   ��6?   �� *DeleteCommand+5C      5D      5E      