   M S B A M L   `   `   `  ����G  CComixShopWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ComixShopWPF.Views.WindowsComixShopWPF.Views.Windows  5   	   a5clr-namespace:ComixShopWPF.ViewModels.AdminViewModels'ComixShopWPF.ViewModels.AdminViewModels  5   	   1    +ComixShopWPF.Views.Windows.ComixAdminWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:ComixShopWPF.Views.Windows     5   	   Dvm5clr-namespace:ComixShopWPF.ViewModels.AdminViewModels     5   	     � Title$  ComixAdminWindow��5	   	   $
��auto��6"   $	��800��60   .��5
      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   ��  �� Orientation$ Vertical=�6    � Cursor$  ��6,   .+�5      �� $��Имя��6   $
��Bold"�6#   $��5q�65   �� 5      ��6   �� *Comix.Title+�� 5      $��Описание��6   $
��Bold"�6(   $��5q�6:   
�������6E   �� 5      ��6   �� *Comix.Description+�� 5      $��Категория��6   $
��Bold"�6)   $��5q�6;   
�������6F   �� 5      $
{�True��5       �� SelectedIndex$ 0��6"   ��5      �� *CategoryList+ �� SelectedItem 5      �� *Comix.Genre+ �� Mode$ TwoWay=��� 5      $��
Автор��6   $
��Bold"�6%   $��5q�67   
�������6B   �� 5      ��6   �� *Comix.Author+�� 5!      $��Цена��6   $
��Bold"�6$   $��5q�66   
�������6A   �� 5"      -   5        $��priceBox��5"      ��6&   �� *Comix.Price+�� 5$      $��Количество��6   $
��Bold"�6*   $��5q�6<   
�������6G   �� 5%      -   5        $��quaBox��5%      ��6$   �� *Comix.StockQuantity+�� 51      $^��XИзображение введите URL изображения или выберите��6   $
��Bold"�6P   $��5q�6b   
�������6m   �� 52      $	��300��54      $��0,5,0,5q�55      $
��Left=�56      ��53      �� *Comix.ImageUrl+56      �� 58      $	��150��5:      $	��150��5;      $��10q�5<      ��59      �� *Comix.ImageUrl+ �� UpdateSourceTrigger$ PropertyChanged=�5<      5=      �� 5>   
   -   5        $��1��5?      $��Подтвердить��5@      $	��150��5B      $��10q�5C      5E      5F      