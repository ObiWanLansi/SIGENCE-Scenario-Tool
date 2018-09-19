

% A class with all known RxTxTypes as Property.
classdef RxTxTypes
   properties


        % Unknown RxTxType.
        Unknown = 4242

        % Ideal Sdr Receiver (Passes Signal Through).
        IdealSDR = 1

        % HackRF One.
        HackRF = 2

        % Ettus B200mini.
        B200mini = 3

        % Ettus X310 / TwinRx.
        TwinRx = 4

        % QPSK Signal With 2kHz Bandwidth.
        QPSK = 1

        % This Is A Sine Generator A 500Hz Frequency.
        SIN = 2

        % This Is A Fm Broadcast Radio Transmitter (Awgn Noise Signal) With Input 20Khz Signal And 50Khz Bandwidth.
        FMBroadcast = 3

        % 10MHz L1 GPS Jammer.
        GPSJammer = 4

        % Iridium Satcom Transmitter.
        Iridium = 5

        % LTE Signal.
        LTE = 6

        % AIS Signal.
        AIS = 7

        % Narrow Fm Band (Voice With 5Khz Bandwidth).
        NFMRadio = 8

   end
end
