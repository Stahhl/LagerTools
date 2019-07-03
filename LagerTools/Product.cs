using System;
using System.Collections.Generic;
using System.Text;

public enum Category
{
    NULL,
    Meat,
    Fish,
    Fruit,
    Vegetable,
    Bread,
    Other,
    //TODO ADD MORE
}
public enum Storage
{
    NULL,

    A1,A2,A3,A4,A5,A6,A7,A8,A9, //A

    B1,B2,B3,B4,B5,B6,B7,B8,B9, //B

    C1,C2,C3,C4,C5,C6,C7,C8,C9, //C

    D1,D2,D3,D4,D5,D6,D7,D8,D9, //D

    E1,E2,E3,E4,E5,E6,E7,E8,E9, //E

    F1,F2,F3,F4,F5,F6,F7,F8,F9, //F

    G1,G2,G3,G4,G5,G6,G7,G8,G9, //G

    H1,H2,H3,H4,H5,H6,H7,H8,H9, //H

    I1,I2,I3,I4,I5,I6,I7,I8,I9, //I

    J1,J2,J3,J4,J5,J6,J7,J8,J9, //J

    K1,K2,K3,K4,K5,K6,K7,K8,K9, //K

    L1,L2,L3,L4,L5,L6,L7,L8,L9, //L

    M1,M2,M3,M4,M5,M6,M7,M8,M9, //M

    N1,N2,N3,N4,N5,N6,N7,N8,N9, //N

    O1,O2,O3,O4,O5,O6,O7,O8,O9, //O

    P1,P2,P3,P4,P5,P6,P7,P8,P9, //P

    Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9, //Q

    R1,R2,R3,R4,R5,R6,R7,R8,R9, //R

    S1,S2,S3,S4,S5,S6,S7,S8,S9, //S

    T1,T2,T3,T4,T5,T6,T7,T8,T9, //T

    U1,U2,U3,U4,U5,U6,U7,U8,U9, //U

    V1,V2,V3,V4,V5,V6,V7,V8,V9, //V

    W1,W2,W3,W4,W5,W6,W7,W8,W9, //W

    X1,X2,X3,X4,X5,X6,X7,X8,X9, //X

    Y1,Y2,Y3,Y4,Y5,Y6,Y7,Y8,Y9, //Y

    Z1,Z2,Z3,Z4,Z5,Z6,Z7,Z8,Z9, //Z
}

namespace LagerTools
{
    public class Product
    {
        //produktnummer, produktkategori, produktnamn, plats
        public Product(string ProductNumber, Category ProductCategory, string ProductName, Storage ProductStorage)
        {
            this.ProductNumber = ProductNumber;
            this.ProductCategory = ProductCategory;
            this.ProductName = ProductName;
            this.ProductStorage = ProductStorage;
        }

        public string ProductNumber { get; private set; }
        public Category ProductCategory { get; private set; }
        public string ProductName { get; private set; }
        public Storage ProductStorage { get; private set; }
    }
}
