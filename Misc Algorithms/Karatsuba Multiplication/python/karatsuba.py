def karatsuba_multiply(x, y):
    """Function to multiply 2 numbers in a more efficient manner than the grade school algorithm"""
    if len(str(x)) == 1 or len(str(y)) == 1:
        return x * y
    else:
        n = max(len(str(x)), len(str(y)))
        nby2 = n // 2
        ten_pow_nby2 = 10**nby2
        a = x // ten_pow_nby2
        b = x % ten_pow_nby2
        c = y // ten_pow_nby2
        d = y % ten_pow_nby2

        ac = karatsuba_multiply(a, c)
        bd = karatsuba_multiply(b, d)
        ad_plus_bc = karatsuba_multiply(a + b, c + d) - ac - bd

        # this little trick, writing n as 2*nby2 takes care of both even and odd n
        prod = ac * 10 ** (2 * nby2) + (ad_plus_bc * 10 ** nby2) + bd

        return prod
