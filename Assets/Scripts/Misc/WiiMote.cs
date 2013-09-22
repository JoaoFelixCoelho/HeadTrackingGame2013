using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

public static class WiiMote
{

    [DllImport("UniWii")]
    public static extern void wiimote_start();
    [DllImport("UniWii")]
    public static extern void wiimote_stop();

    [DllImport("UniWii")]
    public static extern int wiimote_count();
    [DllImport("UniWii")]
    public static extern bool wiimote_available(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_isIRenabled(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_enableIR(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_isExpansionPortEnabled(int which);
    [DllImport("UniWii")]
    public static extern void wiimote_rumble(int which, float duration);
    [DllImport("UniWii")]
    public static extern double wiimote_getBatteryLevel(int which);

    [DllImport("UniWii")]
    public static extern byte wiimote_getAccX(int which);
    [DllImport("UniWii")]
    public static extern byte wiimote_getAccY(int which);
    [DllImport("UniWii")]
    public static extern byte wiimote_getAccZ(int which);

    [DllImport("UniWii")]
    public static extern float wiimote_getIrX(int which);
    [DllImport("UniWii")]
    public static extern float wiimote_getIrY(int which);
    [DllImport("UniWii")]
    public static extern float wiimote_getRoll(int which);
    [DllImport("UniWii")]
    public static extern float wiimote_getPitch(int which);
    [DllImport("UniWii")]
    public static extern float wiimote_getYaw(int which);

    [DllImport("UniWii")]
    public static extern byte wiimote_getNunchuckStickX(int which);
    [DllImport("UniWii")]
    public static extern byte wiimote_getNunchuckStickY(int which);

    [DllImport("UniWii")]
    public static extern byte wiimote_getNunchuckAccX(int which);
    [DllImport("UniWii")]
    public static extern byte wiimote_getNunchuckAccZ(int which);

    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonA(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonB(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonUp(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonLeft(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonRight(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonDown(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButton1(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButton2(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonNunchuckC(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonNunchuckZ(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonPlus(int which);
    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonMinus(int which);

    // Aquired through Dependency walker. 

    [DllImport("UniWii")]
    public static extern bool wiimote_getButtonHome(int which);

}

