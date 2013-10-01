using System;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
/*
 * $Id: AESCipher.cs 318 2012-02-27 22:46:07Z psoares33 $
 *
 * This file is part of the iText project.
 * Copyright (c) 1998-2012 1T3XT BVBA
 * Authors: Bruno Lowagie, Paulo Soares, et al.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License version 3
 * as published by the Free Software Foundation with the addition of the
 * following permission added to Section 15 as permitted in Section 7(a):
 * FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY 1T3XT,
 * 1T3XT DISCLAIMS THE WARRANTY OF NON INFRINGEMENT OF THIRD PARTY RIGHTS.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Affero General Public License for more details.
 * You should have received a copy of the GNU Affero General Public License
 * along with this program; if not, see http://www.gnu.org/licenses or write to
 * the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 * Boston, MA, 02110-1301 USA, or download the license from the following URL:
 * http://itextpdf.com/terms-of-use/
 *
 * The interactive user interfaces in modified source and object code versions
 * of this program must display Appropriate Legal Notices, as required under
 * Section 5 of the GNU Affero General Public License.
 *
 * In accordance with Section 7(b) of the GNU Affero General Public License,
 * you must retain the producer line in every PDF that is created or manipulated
 * using iText.
 *
 * You can be released from the requirements of the license by purchasing
 * a commercial license. Buying such a license is mandatory as soon as you
 * develop commercial activities involving the iText software without
 * disclosing the source code of your own applications.
 * These activities include: offering paid services to customers as an ASP,
 * serving PDFs on the fly in a web application, shipping iText with a closed
 * source product.
 *
 * For more information, please contact iText Software Corp. at this
 * address: sales@itextpdf.com
 */
namespace iTextSharp.text.pdf.crypto {

/**
 * Creates an AES Cipher with CBC and no padding.
 * @author Paulo Soares
 */
    public class AESCipherCBCnoPad {
        private IBlockCipher cbc;
        
        /** Creates a new instance of AESCipher */
        public AESCipherCBCnoPad(bool forEncryption, byte[] key) {
            IBlockCipher aes = new AesFastEngine();
            cbc = new CbcBlockCipher(aes);
            KeyParameter kp = new KeyParameter(key);
            cbc.Init(forEncryption, kp);
        }
        
        public byte[] ProcessBlock(byte[] inp, int inpOff, int inpLen) {
            if ((inpLen % cbc.GetBlockSize()) != 0)
                throw new ArgumentException("Not multiple of block: " + inpLen);
            byte[] outp = new byte[inpLen];
            int baseOffset = 0;
            while (inpLen > 0) {
                cbc.ProcessBlock(inp, inpOff, outp, baseOffset);
                inpLen -= cbc.GetBlockSize();
                baseOffset += cbc.GetBlockSize();
                inpOff += cbc.GetBlockSize();
            }
            return outp;
        }        
    }
}
