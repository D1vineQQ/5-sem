   M S B A M L   `   `   `  ����G  CComixShopWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:ComixShopWPF.Views.Frames.ProfileViews&ComixShopWPF.Views.Frames.ProfileViews  5      e7clr-namespace:ComixShopWPF.ViewModels.ProfileViewModels)ComixShopWPF.ViewModels.ProfileViewModels  5      <    6ComixShopWPF.Views.Frames.ProfileViews.ProfileInfoView   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Flocal4clr-namespace:ComixShopWPF.Views.Frames.ProfileViews     5      Hpfvm7clr-namespace:ComixShopWPF.ViewModels.ProfileViewModels     5        � DataContext  5
      D   >ComixShopWPF.ViewModels.ProfileViewModels.ProfileInfoViewModel  5   
   .��5      � $��40q�6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��1* �6   .+�5   
   �� $,��&Информация о профиле��6   $��20(�60   $
��Bold"�6>   $��Center=�6P   $��0 0 0 20q�6m   �� 5   
   $��0��6   $��0 30 0 0q�6"   .+�5      �� $��
Логин��6   $
��Bold"�6%   $	��0 5q�67   �� 5       �� 
IsReadOnly$
 True��6<   ���#E0E0E0��6   �� *CurrentUser.Username+6N   �� 5      $��Имя��6   $
��Bold"�6#   $	��0 5q�65   �� 5      ��6   �� *CurrentUser.FirstName+�� 5      $��Фамилия��6   $
��Bold"�6'   $	��0 5q�69   �� 5      ��6   �� *CurrentUser.LastName+�� 5      $��Email��6   $
��Bold"�6%   $	��0 5q�67   �� 5      ��6   �� *CurrentUser.Email+5       �� 5"   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5#      �� $��Сохранить��6   $	��110��6P   $��10q�6\   ��6)   �� *SaveProfileCommand+6\   �� 5$      $��Обновить��6   $	��110��6O   $��10q�6[   ��6(   �� *LoadProfileCommand+6[   5%      5&      5'      