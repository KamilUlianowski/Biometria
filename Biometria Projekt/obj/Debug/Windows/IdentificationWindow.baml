   M S B A M L   `   `   `  ����L  HBiometria Projekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Biometria_Projekt.WindowsBiometria_Projekt.Windows  5   	   4    .Biometria_Projekt.Windows.IdentificationWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Biometria_Projekt.Windows     5   	     � Title$  IdentificationWindow��5   	   $	��300��6&   $	��300��63    � WindowStartupLocation$ CenterScreen=�6?   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��0.2* �6   6,   �� 5      $
��0.8* �6   6,   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   .+�5      �� $��5q�6   .��6   Liczba próbek6/   �� 5      -   5        $��NumberOfSamples��5      $��5q�6-   $
��10,0q�68    �� TextAlignment$
 Left=�6G   6^   �� 5      -   5        $��ConfirmButton��5      $��5q�6H   $
��25,0q�6S   .��6b   Ok6f   5      �� 5   
   -   5        $��UserText��5      $��1��6"   $��5q�6/    �� TextWrapping$
 Wrap=�6:   5   J   5      5      