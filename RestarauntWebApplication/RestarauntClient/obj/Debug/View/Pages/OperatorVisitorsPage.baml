   M S B A M L   `   `   `  ����K  GRestarauntClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:RestarauntClient.View.PagesRestarauntClient.View.Pages  5      6    0RestarauntClient.View.Pages.OperatorVisitorsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:RestarauntClient.View.Pages     5        N� Title$  OperatorVisitorsPage��5	         
LightGreen N� 
Background# [  5
      .��5      � $	��5pxq�5   	    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��5        TextBlockHeader1#��[ 5      $��Список гостей��5      � 5   
   $��1��5       � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��auto �6#   �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5       �� $
��25px��5"      $��Stretch=�5#      $��Center=�5$      $��0 5 5 5q�5%      ��5!      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�5%      �� 5&      $
��25px��5)      $��1��5*      $��150px��5+      $��Center=�5,      $��Center=�5-      $	��5pxq�5.      ��5'      �� *DateOfBookingFiltration+ �� SelectedItem 5(      �� *SelectedDateOfBooking+5.      �� 5/      $
��25px��52      $��2��53      $��150px��54      $��Stretch=�55      $��Center=�56      $	��5pxq�57       �� DisplayMemberPath$ TableDescription��58      ��50      �� *TableOfBookingFiltration+ 51      �� *SelectedTableOfBooking+58      �� 59      $��3��5;        DefaultButton#��[ 5<      $
��25px��5=      $��100px��5>      $��Удалить��5?      $��5 5 0 5q�5@      ��5:      �� *DeleteBooking+5@      5A      �� 5C   
   $��2��5F      #��[  5G      $��Stretch=�5H      $��0q�5I       5D      �� *SelectedBooking+��5E      �� *VisitorsViewList+��5J      �� 5K      .R�5L      ��  	 Green#��[ 5M       	 Brown#��[ 5N      $	��1.5q�5O       �� CornerRadius$ 5��5P      ._�5Q      � $��50��5R       5S      �� 5T   "   $��* �63   �� 5U   "   $��* �63   �� 5V   "   $��* �63   �� 5W   "   $��* �63   �� 5X   "   $��* �63   .+�5\      �� $��Center=�5]   !   $��Center=�5^   !   $��0��5_   !   $��5q�5`   !    �� TextAlignment$
 Left=�5a   !     TextBlockDefault#��[ 5c   !   ��5b   !   �� 	 �� Path$	 Visitor.VisitorFullName�5c   !   �� 5d      $��Center=�5e   !   $��Center=�5f   !   $��1��5g   !   $��5q�5h   !   $
 Left=�5i   !   #��[ 5k   !   ��5j   !   �� $	 Visitor.VisitorEmail�5k   !   �� 5l      $��Center=�5m   !   $��Center=�5n   !   $��2��5o   !   $��5q�5p   !   $
 Left=�5q   !   #��[ 5s   !   ��5r   !   �� $	 Visitor.VisitorTelephone�5s   !   � 5u      $��3��5v   !    5w   "   �� 5x   &   $��* �64   �� 5y   &   $��* �64   .+�5{   "   �� $��0��5|   %   $��Center=�5}   %   $��Center=�5~   %   $��5q�5   %   $
 Left=�5�   %   #��[ 5�   %   ��5�   %   �� $	 DateBooking�
 �� ConverterCulture$
 ru-RU�� �� StringFormat$  Дата: {0:dd MMMM yyyy}��5�   %   �� 5�   "   $��1��5�   %   $��Center=�5�   %   $��Center=�5�   %   $��5q�5�   %   $
 Left=�5�   %   #��[ 5�   %   ��5�   %   �� $	 DateBooking�$
 ru-RU��$ Время: {0:HH':'mm}��5�   %   5�      �� 5�      $��Center=�5�   !   $��Center=�5�   !   $��4��5�   !   $��5q�5�   !   $
 Left=�5�   !   #��[ 5�   !   ��5�   !   �� $! Столик номер {0}��$	 Table.TableDescription�5�   !   5�      5�      5�      5�      5�      5�      