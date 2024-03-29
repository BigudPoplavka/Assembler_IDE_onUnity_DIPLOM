﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class DescriptBase
{
    public static string descr_ID = "Идентификация (бит 21). Возможность программно устанавливать и сбрасывать этот флаг служит признаком того, что процессор поддерживает инструкцию CPUID";
    public static string descr_AC = "Проверка выравнивания (бит 18). Когда одновременно установлены этот флаг и бит AC управляющего регистра CR0, а программа выполняется на третьем уровне привилегий, активизируется проверка выравнивания операндов, расположенных в памяти. ";
    public static string descr_VM = "Режим виртуального процессора 8086 (бит 17). Когда этот флаг установлен, процессор работает в режиме виртуального процессора 8086";
    public static string descr_RF = "Флаг возобновления (бит 16). Управляет реакцией процессора на точки останова. Когда установлен, запрещает генерацию отладочных прерываний (#DB). Основной функцией флага RF является обеспечение повторного выполнения инструкции после возникновения отладочного прерывания, вызванного точкой останова. ";
    public static string descr_NT = "Вложенная задача (бит 14). Этот флаг устанавливается процессором автоматически при переключении на новую задачу.";
    public static string descr_IOPL = "Поле уровня привилегий ввода/вывода (биты 12 и 13). Содержит уровень привилегий ввода-вывода для выполняемой в настоящий момент программы. Если текущий уровень привилегий (CPL) программы численно меньше либо равен значению, находящемуся в поле IOPL, программа может использовать инструкции ввода-вывода, а также менять состояние флага IF. ";
    public static string descr_OF = "Флаг переполнения (бит 11). Устанавливается, если в результате арифметической операции зафиксировано знаковое переполнение, то есть если результат, рассматриваемый как число со знаком, не помещается в операнд-приёмник. Если знакового переполнения нет, этот флаг сбрасывается";
    public static string descr_DF = "Флаг направления (бит 10). Когда этот флаг сброшен, строковые операции (MOVS, CMPS, SCAS, LODS и STOS) обрабатывают данные в порядке возрастания адресов (увеличивая содержимое регистров SI/ESI/RSI и DI/EDI/RDI после каждой итерации), а когда установлен — в порядке убывания адресов";
    public static string descr_IF = "Флаг разрешения прерывания (бит 9). Когда установлен, процессор обрабатывает маскируемые прерывания, запросы на которые поступают от контроллера прерываний или улучшенного контроллера прерываний. Когда сброшен, маскируемые прерывания процессором игнорируются";
    public static string descr_TF = "Флаг ловушки (бит 8). Когда установлен, вызывает прерывание #DB (вектор 1) после выполнения каждой команды процессора за исключением той, которая осуществила установку этого флага";
    public static string descr_SF = "Флаг знака (бит 7). Устанавливается, если в результате операции получено отрицательное число, т.е. если старший разряд результата равен единице. В противном случае сбрасывается";
    public static string descr_ZF = "Флаг нуля (бит 6). Устанавливается при получении нулевого результата, сбрасывается в противном случае.";
    public static string descr_AF = "Флаг вспомогательного переноса (бит 4). Устанавливается при возникновении переноса или заёма из 4-ого раззряда в 3-ий разряд. Сбрасывается при отсутствии такового.";
    public static string descr_PF = "Флаг чётности (бит 2). Устанавливается, если младший байт результата содержит чётное число единичных битов, в противном случае сбрасывается";
    public static string descr_CF = "Флаг переноса (бит 0). В арифметических операциях над целыми числами этот флаг, будучи установленным, показывает наличие переноса или заёма (это можно рассматривать как «беззнаковое переполнение»), а будучи сброшеннм — отсутствие переноса или заёма. ";

    public static string descr_FLAGS = "Флаги – это отдельные биты, принимающие значение 0 или 1. ";
    public static string descr_AX = "Регистр АX предназначен для временного хранения данных (регистр аккумулятор); часто используется при выполнении операций сложения, вычитания, сравнения и других арифметических и логических операции";
    public static string descr_BX = "Регистр ВX предназначен для хранения адреса некоторой области памяти (базовый регистр), а также используется как вычислительный регистр";
    public static string descr_CX = "Регистр СX иногда используется для временного хранения данных, но в основном используется счетчиком; в нем хранится число повторений одной команды или фрагмента программы";
    public static string descr_DX = "Регистр DX используется главным образом для временного хранения данных; часто используется средством пересылки данных между разными программными системами";
    public static string descr_SI = "Регистр является индексом назначения и применяется также для строковых операций. В данном случае он используется совместно с регистром ES";
    public static string descr_DI = "Регистр является индексом источника данных и применяется для некоторых операций над строками. В этом случае он адресует память в паре с регистром DS";
    public static string descr_SP = "Указатель стека SP – это 16-битовый регистр, который определяет смещение текущей вершины стека. Указатель стека SP вместе с сегментным регистром стека SS используются микропроцессором для формирования физического адреса стека";
    public static string descr_BP = "Указатель базы ВР - это 16-битовый регистр, облегчает доступ к параметрам (данным и адресам, переданным через стек)";
    public static string descr_IP = "Регистр указателя команд IP, иначе называемый регистром счетчика команд, имеет размер 16 бит и хранит адрес некоторой ячейки памяти – начало следующей команды";
    public static string descr_CS = "CS — регистр программного сегмента (сегмента кода) определяет место части памяти, где находится программа, т. е. выполняемые процессором команды";
    public static string descr_DS = "DS — регистр информационного сегмента (сегмента данных) идентифицирует часть памяти, предназначенной для хранения данных";
    public static string descr_ES = "SS — регистр стекового сегмента (сегмента стека) определяет часть памяти, используемой как системный стек";
    public static string descr_SS = "ES — регистр расширенного сегмента (дополнительного сегмента) указывает дополнительную область памяти, используемую для хранения данных";

    public static string descr_AL = "AL (младшая часть AX) размером 8 бит (1 байт)";
    public static string descr_AH = "АН (старшая часть AX) размером 8 бит (1 байт)";
    public static string descr_BL = "BL (младшая часть BX) размером 8 бит (1 байт)";
    public static string descr_BH = "BН (старшая часть BX) размером 8 бит (1 байт)";
    public static string descr_CL = "CL (младшая часть CX) размером 8 бит (1 байт)";
    public static string descr_CH = "CН (старшая часть CX) размером 8 бит (1 байт)";
    public static string descr_DL = "DL (младшая часть DX) размером 8 бит (1 байт)";
    public static string descr_DH = "DН (старшая часть DX) размером 8 бит (1 байт)";
}

