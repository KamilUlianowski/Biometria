   M S B A M L   `   `   `  ����L  HBiometria Projekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Biometria_Projekt.WindowsBiometria_Projekt.Windows  5   	   2    ,Biometria_Projekt.Windows.IdentityUserWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Biometria_Projekt.Windows     5   	     � Title$  IdentityUserWindow��5   	   $	��300��6$   $	��300��61    � WindowStartupLocation$ CenterScreen=�6=   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��0.2* �6   6,   �� 5      $
��0.2* �6   6,   �� 5      $
��0.6* �6   6,   .+�5   
   �� -   5        $��StackPanelSamples��5       �� Orientation$ 
Horizontal=�6.   $��0��6G   .+�5      �� $��5q�6   $��Center=�6   $��Center=�5      .��68   Liczba próbek6G   �� 5      -   5        $��NumberOfSamples��5      $��5q�6-   $��50��68   $��Center=�6C   $��Center=�5       �� TextAlignment$ Center=�68   6Q   �� 5      -   5        $��ConfirmButtonSamples��5   1   $��Right=�6   $��5q�6M   $
��15,0q�6X   .��6�   Ok6�   5      �� 5   
   -   5        $��StackPanelTests��5      $ 
Horizontal=�6,   $��1��6E   .+�5      �� $��5q�6   $��Center=�6   $��Center=�5      .��68   Liczba testów6G   �� 5      -   5        $��NumberOfTests��5      $��8,5,5,5q�6+   $��50��6<   $��Center=�6G   $��Center=�5      $ Center=�68   6Q   �� 5      -   5        $��ConfirmButton��5   1   $��Right=�6   $��5q�6F   $
��15,0q�6Q   .��6|   Ok6�   5      �� 5!   
   -   5        $��UserText��5!      $��2��6"   $��5q�6/    �� TextWrapping$
 Wrap=�6:   5"   J   5#      5$      