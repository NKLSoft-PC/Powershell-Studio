import sys,ctypes,platform

def is_admin():
    try:
        return ctypes.windll.shell32.IsUserAnAdmin()
    except:
        raise False

if __name__ == '__main__':

    if platform.system() == "Windows":
        if is_admin():
            main(sys.argv[1:])
        else:
            # Re-run the program with admin rights, don't use __file__ since py2exe won't know about it
            # Use sys.argv[0] as script path and sys.argv[1:] as arguments, join them as lpstr, quoting each parameter or spaces will divide parameters
            lpParameters = ""
            # Litteraly quote all parameters which get unquoted when passed to python
            for i, item in enumerate(sys.argv[0:]):
                lpParameters += '"' + item + '" '
            try:
                ctypes.windll.shell32.ShellExecuteW(None, "runas", sys.executable, lpParameters , None, 1)
            except:
                sys.exit(1)
    else:
        main(sys.argv[1:])