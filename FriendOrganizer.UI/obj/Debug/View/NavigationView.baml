   M S B A M L   `   `   `  ����M  IFriendOrganizer.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:FriendOrganizer.UI.ViewFriendOrganizer.UI.View  5      ,    &FriendOrganizer.UI.View.NavigationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:FriendOrganizer.UI.View     5        � 	Resources  5	         NaviItemContainerStyle �� 
TargetType �� Property �� Value  NaviItemTemplate �� 
TargetType �Name �� Triggers P� Property P� Value��%  &	        &	 m     �� 5
   
   " ��6/   .�5      ��  � ��$
 left=�64   �� 5       � ��$ 5q�6'   5      �� 5   
   .R�5      �� $��18(�5      ��5      �� *DisplayMember+��5      �� *OpenDetailViewCommand+��5      �� 5      " ��6&   .R�5      � $
 grid��6   .+�5      �� #��z��6/   #��z��5   ;   5       5      P� 5       �@J�IsMouseOver . . �5   "   ��  �@�Cursor$
 Hand��6;   �� 5   "    � ��$
 Bold"�6?   5      P� 5       � �� . . �5   "   ��  � ��$ Normal"�6?   5       5"      5%      5&      5'      .��5(      � 	 � RowDefinitions	 5)   
   �� 5*      $	��300 �6   �� 5+      $��* �6   .+�5-   
   �� $��0��6   $��Друзья��6    .��5.      �� $
��Auto=�6   $
��Auto=�5/      .��50      �� #��[  51      #��[ 52      $��10q�53      ��50      �� *	Friends+53      54      55      �� 56   
   $��1��6   $��Встречи��6    .��57      �� $
��Auto=�6   $
��Auto=�58      .��59      �� #��[  5:      #��[ 5;      $��10q�5<      ��59      �� *
Meetings+5<      5=      5>      5?      5@      